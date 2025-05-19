import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, NavLink } from 'react-router-dom';
import axios from 'axios';

export const ItmpModPage =()=> {
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
                const response = await axios.get(`https://itmp.sulla.hu/users/${id}`);
                setItmp(response.data);
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

    const handleSubmit = event => {
        event.preventDefault();
        try {
        axios.put(
            `https://itmp.sulla.hu/users/${id}`,
            itmp,
            { headers: { 'Content-Type': 'application/json' } }
          );
          navigate("/");
        } catch(error) {
          console.log('Error updating data:', error.response?.data || error.message);
        }
    };
    return (
        <div className="p-5 content bg-whitesmoke text-center">
            <h2>Egy ITMP-s bejegyzés módosítása</h2>
            <form onSubmit={handleSubmit}>
                
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">ITMP bejegyzés neve:</label>
                    <div className="col-sm-9">
                        <input type="text" name="name" className="form-control" defaultValue={itmp.name} onChange={handleInputChange}/>
                    </div>
                </div>
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">E-mail cím:</label>
                    <div className="col-sm-9">
                        <input type="text" name="email" className="form-control" defaultValue={itmp.email} onChange={handleInputChange}/>
                    </div>
                </div>
                <NavLink to="/"><i className="bi bi-backspace btn btn-danger"></i></NavLink>
                &nbsp;&nbsp;&nbsp; <button type="submit" className="btn btn-success">Küldés</button>
                
            </form>
        </div>
    );
};