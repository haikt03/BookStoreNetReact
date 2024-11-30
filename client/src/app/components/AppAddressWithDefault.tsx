import { useEffect, useState } from "react";
import axios from "axios";
import { MenuItem, TextField, Grid } from "@mui/material";
import { toast } from "react-toastify";
import { Address } from "../models/address";

interface AppAddressWithDefaultProps {
    address: Address;
    onChange: (address: Address) => void;
    defaultCity?: string;
    defaultDistrict?: string;
    defaultWard?: string;
}

export default function AppAddressWithDefault({
    address,
    onChange,
    defaultCity,
    defaultDistrict,
    defaultWard,
}: AppAddressWithDefaultProps) {
    const [cities, setCities] = useState<any[]>([]);
    const [districts, setDistricts] = useState<any[]>([]);
    const [wards, setWards] = useState<any[]>([]);

    useEffect(() => {
        axios
            .get(
                `${import.meta.env.VITE_STATIC_URL as string}/data/vietnam.json`
            )
            .then((response) => {
                setCities(response.data);
            })
            .catch((_error) => toast.error("Lỗi tải địa chỉ"));
    }, []);

    useEffect(() => {
        if (defaultCity) {
            handleCityChange(defaultCity);
        }
    }, [defaultCity]);

    useEffect(() => {
        if (defaultDistrict) {
            handleDistrictChange(defaultDistrict);
        }
    }, [defaultDistrict]);

    useEffect(() => {
        if (defaultWard) {
            handleWardChange(defaultWard);
        }
    }, [defaultWard]);

    const handleCityChange = (cityName: string) => {
        const selectedCity = cities.find((city) => city.Name === cityName);
        setDistricts(selectedCity?.Districts || []);
        setWards([]);
        onChange({
            ...address,
            city: cityName,
            district: "",
            ward: "",
        });
    };

    const handleDistrictChange = (districtName: string) => {
        const selectedDistrict = districts.find(
            (district) => district.Name === districtName
        );
        setWards(selectedDistrict?.Wards || []);
        onChange({
            ...address,
            district: districtName,
            ward: "",
        });
    };

    const handleWardChange = (wardName: string) => {
        onChange({
            ...address,
            ward: wardName,
        });
    };

    return (
        <Grid container>
            <TextField
                name="address.city"
                required
                select
                margin="normal"
                fullWidth
                label="Tỉnh/Thành phố"
                value={address.city}
                onChange={(e) => handleCityChange(e.target.value)}
            >
                <MenuItem value="">
                    <em>Chọn tỉnh/thành phố</em>
                </MenuItem>
                {cities.map((city) => (
                    <MenuItem key={city.Name} value={city.Name}>
                        {city.Name}
                    </MenuItem>
                ))}
            </TextField>

            <TextField
                name="address.district"
                select
                required
                margin="normal"
                fullWidth
                label="Quận/Huyện"
                value={address.district}
                onChange={(e) => handleDistrictChange(e.target.value)}
                disabled={districts.length === 0}
            >
                <MenuItem value="">
                    <em>Chọn quận/huyện</em>
                </MenuItem>
                {districts.map((district) => (
                    <MenuItem key={district.Name} value={district.Name}>
                        {district.Name}
                    </MenuItem>
                ))}
            </TextField>

            <TextField
                name="address.ward"
                required
                select
                margin="normal"
                fullWidth
                label="Phường/Xã"
                value={address.ward}
                onChange={(e) => handleWardChange(e.target.value)}
                disabled={wards.length === 0}
            >
                <MenuItem value="">
                    <em>Chọn phường/xã</em>
                </MenuItem>
                {wards.map((ward) => (
                    <MenuItem key={ward.Name} value={ward.Name}>
                        {ward.Name}
                    </MenuItem>
                ))}
            </TextField>
        </Grid>
    );
}
