module MuiDatePickers

open Fable.Core

[<JSX.Component>]
let DatePickers () =
    JSX.jsx 
        $"""
        import {{ LocalizationProvider }} from '@mui/x-date-pickers';
        import {{ AdapterLuxon }} from '@mui/x-date-pickers/AdapterLuxon';
        import {{ DatePicker }} from '@mui/x-date-pickers/DatePicker';
        import {{ DateCalendar }} from '@mui/x-date-pickers/DateCalendar';
        <LocalizationProvider dateAdapter={{AdapterLuxon}}>
            <DatePicker />
            <DateCalendar />
        </LocalizationProvider>
        """