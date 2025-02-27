import { useEffect, useState } from 'react';
import './App.css';
import { TextField, FormControl, Select, Button, MenuItem } from '@mui/material';
import { DatePicker, LocalizationProvider } from '@mui/x-date-pickers'
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import dayjs, { Dayjs } from 'dayjs';
import 'dayjs/locale/en-gb';

interface Reserva {
    id: number;
    fecha: string;
    cliente: string;
    servicio: string;
}

interface Service {
    name: string;
}

interface Turno {
    hora: number;
}

function App() {
    const [reservas, setreservas] = useState<Reserva[]>();
    const [services, setservices] = useState<Service[]>();
    const [turnos, setturnos] = useState<Turno[]>();
    const [selectedService, setselectedservice] = useState<string>();
    const [selectedClient, setselectedclient] = useState<string>();
    const [selectedDate, setselecteddate] = useState<Dayjs>();
    const [selectedHora, setselectedhora] = useState<string>();

    useEffect(() => {
        GetReservas();
    }, []);

    useEffect(() => {
        GetServices();
    }, []);

    if (services === undefined) {
        return (<div>
            <h1>Sistema de Reservas</h1>
            <p><em>Por favor, inicie el back end para operar</em></p>
        </div>);
    }

    const tableContent = reservas && reservas.length > 0 ? reservas.map((reserva, index) =>
        <tr key={index}>
            <td>{reserva.id}</td>
            <td>{new Date(reserva.fecha).toLocaleString()}</td>
            <td>{reserva.cliente}</td>
            <td>{reserva.servicio}</td>
        </tr>
    )
        : <tr> </tr>;

    const tabla = <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
            <tr>
                <th>Id</th>
                <th>Fecha</th>
                <th>Cliente</th>
                <th>Servicio</th>
            </tr>
        </thead>
        <tbody>
            {tableContent}
        </tbody>
    </table>;

    const formulario = <FormControl fullWidth>
        <h3>Cliente</h3>
        <TextField
            required id="cliente"
            variant="outlined"
            value={selectedClient}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                setselectedclient(event.target.value);
            }} />
        <h3>Servicio</h3>
        <Select
            required
            labelId="servicio-label"
            id="service"
            value={selectedService ? selectedService : ''}
            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                setselectedservice(event.target.value);
            }}        >
            {services.map((servicio, index) =>
                < MenuItem key={index} value={servicio.name}>{servicio.name}</MenuItem>
            )}
        </Select>
        <h3>Fecha</h3>
        <DatePicker
            label="Fecha"
            type="datetime-local"
            value={selectedDate}
            onChange={(value) => { GetTurnos(value); }}
        >
        </DatePicker>

        {turnos && selectedDate &&
            <div>
                <h3>Horario</h3>
                <Select
                    required
                    labelId="hora-label"
                    id="hora"
                    value={selectedHora}
                    onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                        setselectedhora(event.target.value);
                    }}                >
                    {turnos.map((td, index) =>
                        < MenuItem key={index} value={td.hora.toString()}>{td.hora}Hs.</MenuItem>
                    )
                    }
                </Select>
            </div>
        }
  <br />
        <br />
        <Button
            onClick={async () => {

                if (!selectedClient || !selectedService || !selectedDate || !selectedHora) {
                    alert('Datos Incompletos');
                    return;
                }

                const info = await CreateReserva(selectedClient, selectedService, selectedDate, selectedHora);

                if (info.hasError) {
                    alert(info.message);
                }
                else {
                    const array = reservas?.concat(info.data.reserva);
                    setreservas(array);
                    setselectedservice('');
                    setselectedclient('');
                    setselectedhora(9);
                    setselecteddate(null);
                }
            }}
        >
            Crear Reserva
        </Button>
    </FormControl>;

    return (
        <div>
            <LocalizationProvider
                dateAdapter={AdapterDayjs}
                adapterLocale="en-gb">
            <h1>Sistema de Reservas</h1>
            <div>
                <h2>Nueva Reserva</h2>
                {formulario}
            </div>
            <hr />
            <div>
                <h2>Reservas</h2>
                {tabla}
            </div>
                </LocalizationProvider>
        </div>
    );

    async function GetReservas() {
        const response = await fetch('http://localhost:5138/Reservas/GetReservas');
        const info = await response.json();
        setreservas(info.data.reservas);
    }

    async function GetServices() {
        const response = await fetch('http://localhost:5138/Servicios/GetServicios');
        const info = await response.json();
        setservices(info.data.servicios);
    }


    async function GetTurnos(value) {
        setselecteddate(value);
        
        const params = new URLSearchParams({ Fecha: value.toString()});
        const response = await fetch('http://localhost:5138/Turnos/DisponiblesByFecha?' + params);
        const info = await response.json();
        setturnos(info.data.turnosDisponibles);
    }

    async function CreateReserva(selectedClient: string, selectedService: string, selectedDate: Dayjs, selectedHora: string) {

        const newDate = selectedDate.hour(selectedHora).toString();
        const response = await fetch('http://localhost:5138/Reservas/CreateReserva', {
            method: 'post',
            headers: {
                'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            body: JSON.stringify({
                    "Cliente": selectedClient,
                    "Servicio": selectedService,
                    "Fecha": newDate
            })
        });
        const info = await response.json();
        return info;
    }

}

export default App;