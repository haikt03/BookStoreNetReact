import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { currencyFormat, orderSortOptions } from "../../app/utils/utils";
import { setOrderParams } from "./orderSlice";
import { Box, Paper, Slider, Typography } from "@mui/material";
import OrderSearch from "./OrderSearch";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import AppExpandableSection from "../../app/components/AppExpandableSection";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { Dayjs } from "dayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";

interface Props {
    filter: {
        paymentStatuses: string[];
        orderStatuses: string[];
        minAmount: number;
        maxAmount: number;
        orderDateStart: string | null;
        orderDateEnd: string | null;
    };
}

export default function OrderFilter({ filter }: Props) {
    const { orderParams } = useAppSelector((state) => state.order);
    const dispatch = useAppDispatch();

    const [orderStatusesVisibleCount, setOrderStatusesVisibleCount] =
        useState(3);
    const [paymentStatusesVisibleCount, setPaymentStatusesVisibleCount] =
        useState(3);
    const [sortVisibleCount, setSortVisibleCount] = useState(3);
    const [orderDateStart, setOrderDateStart] = useState<Dayjs | null>(null);
    const [orderDateEnd, setOrderDateEnd] = useState<Dayjs | null>(null);
    const [dateError, setDateError] = useState<string | null>(null);

    const handleExpandOrderStatuses = () => {
        if (orderStatusesVisibleCount + 3 > filter.orderStatuses.length) {
            setOrderStatusesVisibleCount(filter.orderStatuses.length);
        } else {
            setOrderStatusesVisibleCount(orderStatusesVisibleCount + 3);
        }
    };
    const handleExpandPaymentStatuses = () => {
        if (paymentStatusesVisibleCount + 3 > filter.paymentStatuses.length) {
            setPaymentStatusesVisibleCount(filter.paymentStatuses.length);
        } else {
            setPaymentStatusesVisibleCount(paymentStatusesVisibleCount + 3);
        }
    };
    const handleExpandSort = () => {
        if (sortVisibleCount + 3 > orderSortOptions.length) {
            setSortVisibleCount(orderSortOptions.length);
        } else {
            setSortVisibleCount(sortVisibleCount + 3);
        }
    };

    const handleCollapseOrderStatuses = () => setOrderStatusesVisibleCount(3);
    const handleCollapsePaymentStatuses = () =>
        setPaymentStatusesVisibleCount(3);
    const handleCollapseSort = () => setSortVisibleCount(3);

    const orderStatusesToShow = filter.orderStatuses.slice(
        0,
        orderStatusesVisibleCount
    );
    const paymentStatusesToShow = filter.paymentStatuses.slice(
        0,
        paymentStatusesVisibleCount
    );
    const sortOptionsToShow = orderSortOptions.slice(0, sortVisibleCount);

    const showExpandOrderStatuses =
        filter.orderStatuses.length > 3 &&
        orderStatusesVisibleCount < filter.orderStatuses.length;
    const showExpandPaymentStatuses =
        filter.paymentStatuses.length > 3 &&
        paymentStatusesVisibleCount < filter.paymentStatuses.length;
    const showExpandSort =
        orderSortOptions.length > 3 &&
        sortVisibleCount < orderSortOptions.length;

    const handleAmountChange = (
        _event: Event | React.SyntheticEvent,
        newValue: number | number[]
    ) => {
        if (Array.isArray(newValue)) {
            dispatch(
                setOrderParams({
                    minAmount: newValue[0],
                    maxAmount: newValue[1],
                })
            );
        }
    };

    const handleAmountChangeCommitted = (
        _event: Event | React.SyntheticEvent,
        newValue: number | number[]
    ) => {
        if (Array.isArray(newValue)) {
            dispatch(
                setOrderParams({
                    minAmount: newValue[0],
                    maxAmount: newValue[1],
                })
            );
        }
    };

    const handleDateChange = (type: "start" | "end", date: Dayjs | null) => {
        if (date) {
            const formattedDate = date.format("YYYY-MM-DD");
            if (type === "start") {
                setOrderDateStart(date);
                dispatch(setOrderParams({ orderDateStart: formattedDate }));
            } else {
                setOrderDateEnd(date);
                dispatch(setOrderParams({ orderDateEnd: formattedDate }));
            }
        }

        if (
            orderDateStart &&
            orderDateEnd &&
            orderDateStart.isAfter(orderDateEnd)
        ) {
            setDateError("Ngày bắt đầu phải trước ngày kết thúc");
        } else {
            setDateError(null);
        }
    };

    return (
        <Box sx={{ mb: 2 }}>
            <OrderSearch />
            <Paper sx={{ px: 2, pt: 2, my: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Sắp xếp theo
                </Typography>
                <RadioButtonGroup
                    selectedValue={orderParams.sort}
                    options={sortOptionsToShow}
                    onChange={(e) =>
                        dispatch(setOrderParams({ sort: e.target.value }))
                    }
                />
                <AppExpandableSection
                    showExpand={showExpandSort}
                    isCollapsed={sortVisibleCount > 3}
                    onExpand={handleExpandSort}
                    onCollapse={handleCollapseSort}
                />
            </Paper>
            <Paper sx={{ p: 2, mb: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Lọc theo giá
                </Typography>
                <Box
                    sx={{
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                    }}
                >
                    <Slider
                        value={[
                            orderParams.minAmount || filter.minAmount,
                            orderParams.maxAmount || filter.maxAmount,
                        ]}
                        onChange={handleAmountChange}
                        onChangeCommitted={handleAmountChangeCommitted}
                        valueLabelDisplay="auto"
                        valueLabelFormat={(value) => `${value}`}
                        min={filter.minAmount}
                        max={filter.maxAmount}
                        step={1000}
                        sx={{ mb: 1 }}
                    />
                    <Box
                        sx={{
                            display: "flex",
                            justifyContent: "space-between",
                            width: "100%",
                        }}
                    >
                        <Typography variant="body2">{`${currencyFormat(
                            orderParams.minAmount || filter.minAmount
                        )}`}</Typography>
                        <Typography variant="body2">{`${currencyFormat(
                            orderParams.maxAmount || filter.maxAmount
                        )}`}</Typography>
                    </Box>
                </Box>
            </Paper>
            <Paper sx={{ p: 2, mb: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Lọc theo ngày
                </Typography>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <Box
                        sx={{
                            display: "flex",
                            flexDirection: "column",
                            gap: 2,
                        }}
                    >
                        <DatePicker
                            label="Ngày bắt đầu"
                            value={orderDateStart}
                            onChange={(date) => handleDateChange("start", date)}
                            format="DD/MM/YYYY"
                        />
                        <DatePicker
                            label="Ngày kết thúc"
                            value={orderDateEnd}
                            onChange={(date) => handleDateChange("end", date)}
                            format="DD/MM/YYYY"
                        />
                        {dateError && (
                            <Typography color="error" variant="body2">
                                {dateError}
                            </Typography>
                        )}
                    </Box>
                </LocalizationProvider>
            </Paper>
            <Paper sx={{ p: 2, mb: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Trạng thái thanh toán
                </Typography>
                <CheckboxButtons
                    items={paymentStatusesToShow}
                    checked={orderParams.paymentStatuses}
                    onChange={(items: string[]) =>
                        dispatch(setOrderParams({ paymentStatuses: items }))
                    }
                />
                <AppExpandableSection
                    showExpand={showExpandPaymentStatuses}
                    isCollapsed={paymentStatusesVisibleCount > 3}
                    onExpand={handleExpandPaymentStatuses}
                    onCollapse={handleCollapsePaymentStatuses}
                />
            </Paper>
            <Paper sx={{ p: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Trạng thái đơn hàng
                </Typography>
                <CheckboxButtons
                    items={orderStatusesToShow}
                    checked={orderParams.orderStatuses}
                    onChange={(items: string[]) =>
                        dispatch(setOrderParams({ orderStatuses: items }))
                    }
                />
                <AppExpandableSection
                    showExpand={showExpandOrderStatuses}
                    isCollapsed={orderStatusesVisibleCount > 3}
                    onExpand={handleExpandOrderStatuses}
                    onCollapse={handleCollapseOrderStatuses}
                />
            </Paper>
        </Box>
    );
}
