import {
    Box,
    Button,
    Paper,
    Step,
    StepLabel,
    Stepper,
    Typography,
} from "@mui/material";
import { useState } from "react";
import ShippingAddressForm from "./ShippingAddressForm";
import PaymentForm from "./PaymentForm";
import Review from "./Review";
import { useForm, FieldValues, FormProvider } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { checkoutValidationSchema } from "./checkoutValidation";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import agent from "../../app/api/agent";
import { clearBasket } from "../basket/basketSlice";
import { LoadingButton } from "@mui/lab";
import { StripeElementType } from "@stripe/stripe-js";
import {
    CardNumberElement,
    useElements,
    useStripe,
} from "@stripe/react-stripe-js";

const steps = [
    "Địa chỉ giao hàng",
    "Kiểm tra lại đơn hàng",
    "Chi tiết thanh toán",
];

export default function Checkout() {
    const [activeStep, setActiveStep] = useState(0);
    const [orderCode, setOrderCode] = useState(0);
    const [loading, setLoading] = useState(false);
    const dispatch = useAppDispatch();
    const [cardState, setCardState] = useState<{
        elementError: { [key in StripeElementType]?: string };
    }>({ elementError: {} });
    const [cardComplete, setCardComplete] = useState<any>({
        cardNumber: false,
        cardExpiry: false,
        cardCvc: false,
    });
    const [paymentMessage, setPaymentMessage] = useState("");
    const [paymentSucceeded, setPaymentSucceeded] = useState(false);
    const { basket } = useAppSelector((state) => state.basket);
    const stripe = useStripe();
    const elements = useElements();

    function getStepContent(step: number) {
        switch (step) {
            case 0:
                return <ShippingAddressForm />;
            case 1:
                return <Review />;
            case 2:
                return (
                    <PaymentForm
                        cardState={cardState}
                        onCardInputChange={onCardInputChange}
                    />
                );
            default:
                throw new Error("Lỗi chuyển tiếp");
        }
    }

    function onCardInputChange(event: any) {
        setCardState({
            ...cardState,
            elementError: {
                ...cardState.elementError,
                [event.elementType]: event.error?.message,
            },
        });
        setCardComplete({
            ...cardComplete,
            [event.elementType]: event.complete,
        });
    }

    const currentValidationSchema = checkoutValidationSchema[activeStep];

    const methods = useForm({
        mode: "onTouched",
        resolver: yupResolver(currentValidationSchema),
    });

    async function submitOrder(data: FieldValues) {
        setLoading(true);
        delete data.specificAddress;
        const { nameOnCard, address } = data;
        if (!basket?.clientSecret || !stripe || !elements) return;
        try {
            const cardElement = elements.getElement(CardNumberElement);
            const paymentResult = await stripe.confirmCardPayment(
                basket.clientSecret,
                {
                    payment_method: {
                        card: cardElement!,
                        billing_details: {
                            name: nameOnCard,
                        },
                    },
                }
            );
            console.log("paymentResult: ", paymentResult);
            if (paymentResult.paymentIntent?.status === "succeeded") {
                const order = await agent.order.createOrder({
                    shippingAddress: address,
                });
                setOrderCode(order.code);
                setPaymentSucceeded(true);
                setPaymentMessage(
                    "Cảm ơn bạn - Chúng tôi đã nhận được thông tin thanh toán"
                );
                setActiveStep(activeStep + 1);
                dispatch(clearBasket());
                setLoading(false);
            } else {
                setPaymentMessage(
                    paymentResult.error?.message || "Thanh toán thất bại"
                );
                setPaymentSucceeded(false);
                setLoading(false);
                setActiveStep(activeStep + 1);
            }
        } catch (error) {
            console.log(error);
            setLoading(false);
        }
    }

    const handleNext = async (data: FieldValues) => {
        if (activeStep === steps.length - 1) {
            await submitOrder(data);
        } else {
            setActiveStep(activeStep + 1);
        }
    };

    const handleBack = () => {
        setActiveStep(activeStep - 1);
    };

    function submitDisabled(): boolean {
        if (activeStep === steps.length - 1) {
            return (
                !cardComplete.cardCvc ||
                !cardComplete.cardExpiry ||
                !cardComplete.cardNumber ||
                !methods.formState.isValid
            );
        } else {
            return !methods.formState.isValid;
        }
    }

    return (
        <FormProvider {...methods}>
            <Paper
                variant="outlined"
                sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}
            >
                <Typography
                    component="h1"
                    variant="h4"
                    align="center"
                    color="primary.light"
                >
                    Thanh toán
                </Typography>
                <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
                    {steps.map((label) => (
                        <Step key={label}>
                            <StepLabel>{label}</StepLabel>
                        </Step>
                    ))}
                </Stepper>
                <>
                    {activeStep === steps.length ? (
                        <>
                            <Typography variant="h5" gutterBottom>
                                {paymentMessage}
                            </Typography>
                            {paymentSucceeded ? (
                                <Typography variant="subtitle1">
                                    Mã đơn hàng của bạn là #{orderCode}.
                                </Typography>
                            ) : (
                                <Button
                                    variant="contained"
                                    onClick={handleBack}
                                >
                                    Quay trở lại
                                </Button>
                            )}
                        </>
                    ) : (
                        <form onSubmit={methods.handleSubmit(handleNext)}>
                            {getStepContent(activeStep)}
                            <Box
                                sx={{
                                    display: "flex",
                                    justifyContent: "flex-end",
                                }}
                            >
                                {activeStep !== 0 && (
                                    <Button
                                        onClick={handleBack}
                                        sx={{ mt: 3, ml: 1 }}
                                    >
                                        Quay trở lại
                                    </Button>
                                )}
                                <LoadingButton
                                    loading={loading}
                                    disabled={submitDisabled()}
                                    type="submit"
                                    variant="contained"
                                    sx={{ mt: 3, ml: 1 }}
                                >
                                    {activeStep === steps.length - 1
                                        ? "Đặt hàng"
                                        : "Tiếp"}
                                </LoadingButton>
                            </Box>
                        </form>
                    )}
                </>
            </Paper>
        </FormProvider>
    );
}
