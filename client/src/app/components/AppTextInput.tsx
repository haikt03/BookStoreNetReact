import { TextField, TextFieldProps } from "@mui/material";
import { useController, UseControllerProps } from "react-hook-form";

interface Props
    extends UseControllerProps,
        Omit<TextFieldProps, "name" | "defaultValue"> {
    label: string;
    multiline?: boolean;
    rows?: number;
    type?: string;
}

export default function AppTextInput(props: Props) {
    const { field, fieldState } = useController({
        ...props,
        defaultValue: props.defaultValue || "",
    });

    return (
        <TextField
            {...field}
            {...props}
            fullWidth
            margin="normal"
            variant="outlined"
            error={!!fieldState.error}
            helperText={fieldState.error?.message}
        />
    );
}
