import { useState } from "react";
import useAuthors from "../../../app/hooks/useAuthors";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Grid, Paper, Typography } from "@mui/material";
import AppPagination from "../../../app/components/AppPagination";
import { setAuthorPageIndex, setAuthorParams } from "../authorSlice";
import AuthorSearch from "./AuthorSearch";
import RadioButtonGroup from "../../../app/components/RadioButtonGroup";
import AppExpandableSection from "../../../app/components/AppExpandableSection";
import CheckboxButtons from "../../../app/components/CheckboxButtons";
import AuthorList from "./AuthorList";

const sortOptions = [
    { value: "fullNameAsc", label: "Tên tác giả (↑)" },
    { value: "fullNameDesc", label: "Tên tác giả (↓)" },
    { value: "countryAsc", label: "Quốc tịch (↑)" },
    { value: "countryDesc", label: "Quốc tịch (↓)" },
];

export default function Author() {
    const { authors, filtersLoaded, filter, metaData } = useAuthors();
    const { authorParams } = useAppSelector((state) => state.author);
    const dispatch = useAppDispatch();

    const [countriesVisibleCount, setCountriesVisibleCount] = useState(5);
    const [sortVisibleCount, setSortVisibleCount] = useState(5);

    const handleExpandCountries = () =>
        setCountriesVisibleCount(countriesVisibleCount + 5);
    const handleExpandSort = () => setSortVisibleCount(sortVisibleCount + 5);

    const handleCollapseCountries = () => setCountriesVisibleCount(5);
    const handleCollapseSort = () => setSortVisibleCount(5);

    const countriesToShow = filter.countries.slice(0, countriesVisibleCount);
    const sortOptionsToShow = sortOptions.slice(0, sortVisibleCount);

    const showExpandCountries = filter.countries.length > 5;
    const showExpandSort = sortOptions.length > 5;

    if (!filtersLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3} />
            <Grid item xs={9}>
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setAuthorPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>

            <Grid item xs={3}>
                <Paper sx={{ mb: 2 }}>
                    <AuthorSearch />
                </Paper>
                <Paper sx={{ p: 2, mb: 2 }}>
                    <Typography variant="h6" gutterBottom color="primary.main">
                        Sắp xếp theo
                    </Typography>
                    <RadioButtonGroup
                        selectedValue={authorParams.sort}
                        options={sortOptionsToShow}
                        onChange={(e) =>
                            dispatch(setAuthorParams({ sort: e.target.value }))
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
                        Quốc tịch
                    </Typography>
                    <CheckboxButtons
                        items={countriesToShow}
                        checked={authorParams.countries}
                        onChange={(items: string[]) =>
                            dispatch(setAuthorParams({ countries: items }))
                        }
                    />
                    <AppExpandableSection
                        showExpand={showExpandCountries}
                        isCollapsed={countriesVisibleCount > 5}
                        onExpand={handleExpandCountries}
                        onCollapse={handleCollapseCountries}
                    />
                </Paper>
            </Grid>
            <Grid item xs={9}>
                <AuthorList authors={authors} />
            </Grid>
            <Grid item xs={3} />

            <Grid item xs={9} sx={{ my: 3 }}>
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setAuthorPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
