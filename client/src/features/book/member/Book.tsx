import { Box, Grid, Paper, Slider, Typography } from "@mui/material";
import useBooks from "../../../app/hooks/useBooks";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import CheckboxButtons from "../../../app/components/CheckboxButtons";
import AppPagination from "../../../app/components/AppPagination";
import { setBookParams, setBookPageIndex } from "../bookSlice";
import BookSearch from "./BookSearch";
import RadioButtonGroup from "../../../app/components/RadioButtonGroup";
import BookList from "./BookList";
import { useState } from "react";
import AppExpandableSection from "../../../app/components/AppExpandableSection";
import { currencyFormat } from "../../../app/utils/utils";
import AppCategoryFilter from "../../../app/components/AppCategoryFilter";

const sortOptions = [
    { value: "nameAsc", label: "Tên sách (↑)" },
    { value: "nameDesc", label: "Tên sách (↓)" },
    { value: "publishedYearAsc", label: "Năm xuất bản (↑)" },
    { value: "publishedYearDesc", label: "Năm xuất bản (↓)" },
    { value: "priceAsc", label: "Giá (↑)" },
    { value: "priceDesc", label: "Giá (↓)" },
    { value: "priceAfterDiscountAsc", label: "Giá sau khi giảm (↑)" },
    { value: "priceAfterDiscountDesc", label: "Giá sau khi giảm (↓)" },
    { value: "discountAsc", label: "Giảm giá (↑)" },
    { value: "discountDesc", label: "Giảm giá(↓)" },
    { value: "quantityInStockAsc", label: "Số lượng trong kho (↑)" },
    { value: "quantityInStockDesc", label: "Số lượng trong kho(↓)" },
];

export default function Book() {
    const { books, filtersLoaded, filter, metaData } = useBooks();
    const { bookParams } = useAppSelector((state) => state.book);
    const dispatch = useAppDispatch();

    const [languagesVisibleCount, setLanguagesVisibleCount] = useState(5);
    const [publishersVisibleCount, setPublishersVisibleCount] = useState(5);
    const [sortVisibleCount, setSortVisibleCount] = useState(5);
    const [selectedCategories, setSelectedCategories] = useState<number[]>([]);

    const handleExpandLanguages = () =>
        setLanguagesVisibleCount(languagesVisibleCount + 5);
    const handleExpandPublishers = () =>
        setPublishersVisibleCount(publishersVisibleCount + 5);
    const handleExpandSort = () => setSortVisibleCount(sortVisibleCount + 5);

    const handleCollapseLanguages = () => setLanguagesVisibleCount(5);
    const handleCollapsePublishers = () => setPublishersVisibleCount(5);
    const handleCollapseSort = () => setSortVisibleCount(5);

    const languagesToShow = filter.languages.slice(0, languagesVisibleCount);
    const publishersToShow = filter.publishers.slice(0, publishersVisibleCount);
    const sortOptionsToShow = sortOptions.slice(0, sortVisibleCount);

    const showExpandLanguages = filter.languages.length > 5;
    const showExpandPublishers = filter.publishers.length > 5;
    const showExpandSort = sortOptions.length > 5;

    const handleCategoryChange = (categories: number[]) => {
        setSelectedCategories(categories);
        dispatch(setBookParams({ categories }));
    };

    const handlePriceChange = (_event: Event, newValue: number | number[]) => {
        if (Array.isArray(newValue)) {
            dispatch(
                setBookParams({ minPrice: newValue[0], maxPrice: newValue[1] })
            );
        }
    };

    if (!filtersLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3} />
            <Grid item xs={9}>
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setBookPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>

            <Grid item xs={3}>
                <BookSearch />
                <Paper sx={{ p: 2, mb: 2 }}>
                    <Typography variant="h6" gutterBottom color="primary.main">
                        Sắp xếp theo
                    </Typography>
                    <RadioButtonGroup
                        selectedValue={bookParams.sort}
                        options={sortOptionsToShow}
                        onChange={(e) =>
                            dispatch(setBookParams({ sort: e.target.value }))
                        }
                    />
                    <AppExpandableSection
                        showExpand={showExpandSort}
                        isCollapsed={sortVisibleCount > 5}
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
                                bookParams.minPrice || filter.minPrice,
                                bookParams.maxPrice || filter.maxPrice,
                            ]}
                            onChange={handlePriceChange}
                            valueLabelDisplay="auto"
                            valueLabelFormat={(value) => `${value}`}
                            min={filter.minPrice}
                            max={filter.maxPrice}
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
                                bookParams.minPrice || filter.minPrice
                            )}`}</Typography>
                            <Typography variant="body2">{`${currencyFormat(
                                bookParams.maxPrice || filter.maxPrice
                            )}`}</Typography>
                        </Box>
                    </Box>
                </Paper>
                <Paper sx={{ p: 2, mb: 2 }}>
                    <Typography variant="h6" gutterBottom color="primary.main">
                        Thể loại
                    </Typography>
                    {filter.categories && (
                        <AppCategoryFilter
                            categories={filter.categories}
                            checkedCategories={selectedCategories}
                            onCategoryChange={handleCategoryChange}
                        />
                    )}
                </Paper>
                <Paper sx={{ p: 2, mb: 2 }}>
                    <Typography variant="h6" gutterBottom color="primary.main">
                        Ngôn ngữ
                    </Typography>
                    <CheckboxButtons
                        items={languagesToShow}
                        checked={bookParams.languages}
                        onChange={(items: string[]) =>
                            dispatch(setBookParams({ languages: items }))
                        }
                    />
                    <AppExpandableSection
                        showExpand={showExpandLanguages}
                        isCollapsed={languagesVisibleCount > 5}
                        onExpand={handleExpandLanguages}
                        onCollapse={handleCollapseLanguages}
                    />
                </Paper>
                <Paper sx={{ p: 2 }}>
                    <Typography variant="h6" gutterBottom color="primary.main">
                        Nhà xuất bản
                    </Typography>
                    <CheckboxButtons
                        items={publishersToShow}
                        checked={bookParams.publishers}
                        onChange={(items: string[]) =>
                            dispatch(setBookParams({ publishers: items }))
                        }
                    />
                    <AppExpandableSection
                        showExpand={showExpandPublishers}
                        isCollapsed={publishersVisibleCount > 5}
                        onExpand={handleExpandPublishers}
                        onCollapse={handleCollapsePublishers}
                    />
                </Paper>
            </Grid>
            <Grid item xs={9}>
                <BookList books={books} />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={9} sx={{ my: 3 }}>
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setBookPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
