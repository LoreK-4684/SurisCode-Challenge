import { useEffect, useState } from 'react';
import './App.css';

interface Reserva {
    id: number;
    fecha: string;
    cliente: string;
    servicio: string;
}

interface Service {
    name: string;
}

function App() {
    const [reservas, setreservas] = useState<Reserva[]>();
    const [services, setservices] = useState<Service[]>();

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
            {reservas.map((reserva, index) =>
            <tr key={index}>
                <td>{reserva.id}</td>
                <td>{new Date(reserva.fecha).toLocaleString()}</td>
                <td>{reserva.cliente}</td>
                <td>{reserva.servicio}</td>
            </tr>
            )}
        </tbody>
    </table>;

    const formulario = <table className="table table-striped" aria-labelledby="formLabel">
        <thead>
            <tr>
                <th>Fecha</th>
                <th>Cliente</th>
                <th>Servicio</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>fecha</td>
                <td>carlos</td>
                <td>
                    <select name="services" id="services">
                        {
                            services.map((servicio, index) =>
                                <option key={index} value={servicio.name}>{servicio.name}</option>)
                        }
                    </select>
                </td>
            </tr>
        </tbody>
    </table>;

    return (
        <div>
            <h1>Sistema de Reservas</h1>
            <div>
                <h2>Nueva Reserva</h2>
                {formulario}
                <br/>
                <button>Crear</button>
            </div>
            <hr />
            <div>
                <h2>Reservas</h2>
                {tabla}
            </div>
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
}

export default App;