import {
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    FormHelperText,
} from "@mui/material";
import { UseControllerProps, useController } from "react-hook-form";

interface SelectItem {
    id: number;
    name: string;
}

interface Props extends UseControllerProps {
    label: string;
    items: SelectItem[];
}

export default function AppSelectList(props: Props) {
    const { fieldState, field } = useController({ ...props, defaultValue: "" });

    return (
        <FormControl fullWidth error={!!fieldState.error}>
            <InputLabel>{props.label}</InputLabel>
            <Select
                value={field.value || ""}
                label={props.label}
                onChange={field.onChange}
            >
                {props.items.map((item) => (
                    <MenuItem value={item.id} key={item.id}>
                        {item.name}
                    </MenuItem>
                ))}
            </Select>
            <FormHelperText>{fieldState.error?.message}</FormHelperText>
        </FormControl>
    );
}
