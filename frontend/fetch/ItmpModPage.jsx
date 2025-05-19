import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, NavLink } from 'react-router-dom';

export const ItmpModPage = () => {
    const params = useParams();
    const id = params.ItmpId;
    const navigate = useNavigate();
    const [itmp, setItmp] = useState({
        name: '',
        email: ''
    });

    useEffect(() => {
        const fetchChessData = async () => {
            try {
                const response = await fetch(`https://itmp.sulla.hu/users/${id}`);
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                const data = await response.json();
                setItmp(data);
            } catch (error) {
                console.log('Hiba az adatok lekérése közben:', error);
            }
        };

        fetchChessData();
    }, [id]);

    const handleInputChange = event => {
        const { name, value } = event.target;
        setItmp(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = async event => {
        event.preventDefault();
        try {
            const response = await fetch(`https://itmp.sulla.hu/users/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(itmp),
            });
            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Hiba az adatfrissítés során');
            }
            navigate("/");
        } catch (error) {
            console.log('Error updating data:', error.message);
        }
    };

    return (
        <div className="p-5 content bg-whitesmoke text-center">
            <h2>Egy ITMP-s bejegyzés módosítása</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">ITMP bejegyzés neve:</label>
                    <div className="col-sm-9">
                        <input
                            type="text"
                            name="name"
                            className="form-control"
                            value={itmp.name}
                            onChange={handleInputChange}
                        />
                    </div>
                </div>
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">E-mail cím:</label>
                    <div className="col-sm-9">
                        <input
                            type="text"
                            name="email"
                            className="form-control"
                            value={itmp.email}
                            onChange={handleInputChange}
                        />
                    </div>
                </div>
                <NavLink to="/">
                    <i className="bi bi-backspace btn btn-danger"></i>
                </NavLink>
                &nbsp;&nbsp;&nbsp; 
                <button type="submit" className="btn btn-success">Küldés</button>
            </form>
        </div>
    );
};
