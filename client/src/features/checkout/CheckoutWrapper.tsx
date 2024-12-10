import { Elements } from "@stripe/react-stripe-js";
import Checkout from "./Checkout";
import { loadStripe } from "@stripe/stripe-js";
import { useState, useEffect } from "react";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useAppDispatch } from "../../app/store/configureStore";
import { setBasket } from "../basket/basketSlice";

const stripePromise = loadStripe(
    import.meta.env.VITE_PUBLISHABLE_KEY as string
);

export default function CheckoutWrapper() {
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        agent.payment
            .createPaymentIntent()
            .then((response) => dispatch(setBasket(response)))
            .catch((error) => console.log(error))
            .finally(() => setLoading(false));
    }, [dispatch]);

    if (loading) return <LoadingComponent />;

    return (
        <Elements stripe={stripePromise}>
            <Checkout />
        </Elements>
    );
}
