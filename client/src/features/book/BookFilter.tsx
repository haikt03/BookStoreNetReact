import { Box, Paper, Slider, Typography } from "@mui/material";
import BookSearch from "./BookSearch";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import { setBookParams } from "./bookSlice";
import AppExpandableSection from "../../app/components/AppExpandableSection";
import { bookSortOptions, currencyFormat } from "../../app/utils/utils";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useState } from "react";

interface Props {
    filter: {
        publishers: string[];
        languages: string[];
        categories: string[];
        minPrice: number;
        maxPrice: number;
    };
}

export default function BookFilter({ filter }: Props) {
    const { bookParams } = useAppSelector((state) => state.book);
    const dispatch = useAppDispatch();

    const [languagesVisibleCount, setLanguagesVisibleCount] = useState(3);
    const [publishersVisibleCount, setPublishersVisibleCount] = useState(3);
    const [categoriesVisibleCount, setCategoriesVisibleCount] = useState(3);
    const [sortVisibleCount, setSortVisibleCount] = useState(3);

    const handleExpandLanguages = () => {
        if (languagesVisibleCount + 3 > filter.languages.length) {
            setLanguagesVisibleCount(filter.languages.length);
        } else {
            setLanguagesVisibleCount(languagesVisibleCount + 3);
        }
    };
    const handleExpandPublishers = () => {
        if (publishersVisibleCount + 3 > filter.publishers.length) {
            setPublishersVisibleCount(filter.publishers.length);
        } else {
            setPublishersVisibleCount(publishersVisibleCount + 3);
        }
    };
    const handleExpandCategories = () => {
        if (categoriesVisibleCount + 3 > filter.categories.length) {
            setCategoriesVisibleCount(filter.categories.length);
        } else {
            setCategoriesVisibleCount(categoriesVisibleCount + 3);
        }
    };
    const handleExpandSort = () => {
        if (sortVisibleCount + 3 > bookSortOptions.length) {
            setSortVisibleCount(bookSortOptions.length);
        } else {
            setSortVisibleCount(sortVisibleCount + 3);
        }
    };

    const handleCollapseLanguages = () => setLanguagesVisibleCount(3);
    const handleCollapsePublishers = () => setPublishersVisibleCount(3);
    const handleCollapseCategories = () => setCategoriesVisibleCount(3);
    const handleCollapseSort = () => setSortVisibleCount(3);

    const languagesToShow = filter.languages.slice(0, languagesVisibleCount);
    const publishersToShow = filter.publishers.slice(0, publishersVisibleCount);
    const categoriesToShow = filter.categories.slice(0, categoriesVisibleCount);
    const sortOptionsToShow = bookSortOptions.slice(0, sortVisibleCount);

    const showExpandLanguages =
        filter.languages.length > 3 &&
        languagesVisibleCount < filter.languages.length;
    const showExpandPublishers =
        filter.publishers.length > 3 &&
        publishersVisibleCount < filter.publishers.length;
    const showExpandCategories =
        filter.categories.length > 3 &&
        categoriesVisibleCount < filter.categories.length;
    const showExpandSort =
        bookSortOptions.length > 3 && sortVisibleCount < bookSortOptions.length;

    const handlePriceChange = (
        _event: Event | React.SyntheticEvent,
        newValue: number | number[]
    ) => {
        if (Array.isArray(newValue)) {
            dispatch(
                setBookParams({ minPrice: newValue[0], maxPrice: newValue[1] })
            );
        }
    };

    const handlePriceChangeCommitted = (
        _event: Event | React.SyntheticEvent,
        newValue: number | number[]
    ) => {
        if (Array.isArray(newValue)) {
            dispatch(
                setBookParams({ minPrice: newValue[0], maxPrice: newValue[1] })
            );
        }
    };

    return (
        <Box sx={{ mb: 2 }}>
            <BookSearch />
            <Paper sx={{ px: 2, pt: 2, my: 2 }}>
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
                            bookParams.minPrice || filter.minPrice,
                            bookParams.maxPrice || filter.maxPrice,
                        ]}
                        onChange={handlePriceChange}
                        onChangeCommitted={handlePriceChangeCommitted}
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
                    isCollapsed={languagesVisibleCount > 3}
                    onExpand={handleExpandLanguages}
                    onCollapse={handleCollapseLanguages}
                />
            </Paper>
            <Paper sx={{ px: 2, pt: 2, mb: 2 }}>
                <Typography variant="h6" gutterBottom color="primary.main">
                    Thể loại
                </Typography>
                <CheckboxButtons
                    items={categoriesToShow}
                    checked={bookParams.categories}
                    onChange={(items: string[]) =>
                        dispatch(setBookParams({ categories: items }))
                    }
                />
                <AppExpandableSection
                    showExpand={showExpandCategories}
                    isCollapsed={categoriesVisibleCount > 3}
                    onExpand={handleExpandCategories}
                    onCollapse={handleCollapseCategories}
                />
            </Paper>
            <Paper sx={{ px: 2, pt: 2, mb: 2 }}>
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
                    isCollapsed={publishersVisibleCount > 3}
                    onExpand={handleExpandPublishers}
                    onCollapse={handleCollapsePublishers}
                />
            </Paper>
        </Box>
    );
}
