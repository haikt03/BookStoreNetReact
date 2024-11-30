import { Paper, Typography } from "@mui/material";
import AuthorSearch from "./AuthorSearch";
import RadioButtonGroup from "../../app/components/RadioButtonGroup";
import { setAuthorParams } from "./authorSlice";
import AppExpandableSection from "../../app/components/AppExpandableSection";
import CheckboxButtons from "../../app/components/CheckboxButtons";
import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { authorSortOptions } from "../../app/utils/utils";

interface Props {
    filter: {
        countries: string[];
    };
}

export default function AuthorFilter({ filter }: Props) {
    const dispatch = useAppDispatch();
    const { authorParams } = useAppSelector((state) => state.author);

    const [countriesVisibleCount, setCountriesVisibleCount] = useState(5);
    const [sortVisibleCount, setSortVisibleCount] = useState(5);

    const handleExpandCountries = () => {
        if (countriesVisibleCount + 5 > filter.countries.length) {
            setCountriesVisibleCount(filter.countries.length);
        } else {
            setCountriesVisibleCount(countriesVisibleCount + 5);
        }
    };
    const handleExpandSort = () => {
        if (sortVisibleCount + 5 > authorSortOptions.length) {
            setSortVisibleCount(authorSortOptions.length);
        } else {
            setSortVisibleCount(sortVisibleCount + 5);
        }
    };

    const handleCollapseCountries = () => setCountriesVisibleCount(5);
    const handleCollapseSort = () => setSortVisibleCount(5);

    const countriesToShow = filter.countries.slice(0, countriesVisibleCount);
    const sortOptionsToShow = authorSortOptions.slice(0, sortVisibleCount);

    const showExpandCountries =
        filter.countries.length > 5 &&
        countriesVisibleCount < filter.countries.length;
    const showExpandSort =
        authorSortOptions.length > 5 &&
        sortVisibleCount < authorSortOptions.length;

    return (
        <>
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
            <Paper sx={{ px: 2, pt: 2, mb: 2 }}>
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
        </>
    );
}
