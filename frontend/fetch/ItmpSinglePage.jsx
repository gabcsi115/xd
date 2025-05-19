import React, { useState, useEffect } from 'react';
import { useParams, NavLink } from 'react-router-dom';

export const ItmpSinglePage = () => {
    const { ItmpId: id } = useParams();
    const [itmp, setItmp] = useState(null);
    const [isPending, setPending] = useState(false);

    useEffect(() => {
        setPending(true);
        fetch(`https://itmp.sulla.hu/users/${id}`)
            .then(res => {
                if (!res.ok) throw new Error(`HTTP error! Status: ${res.status}`);
                return res.json();
            })
            .then(data => setItmp(data))
            .catch(error => console.error(error))
            .finally(() => setPending(false));
    }, [id]);

    return (
        <div className="p-5 m-auto text-center content bg-lavender">
            {isPending || !itmp ? (
                <div className="spinner-border"></div>
            ) : (
                <div className="card p-3">
                    <div className="card-body">
                        <h5 className="card-title">ITMP bejegyzés neve: {itmp.name}</h5>
                        <div className="lead">E-mail cím: {itmp.email}</div>
                        <br />
                    </div>
                    <div>
                        <NavLink to="/" className="btn btn-outline-secondary me-2">
                            <i className="bi bi-backspace"></i> Vissza
                        </NavLink>
                        <NavLink to={`/mod-itmp/${itmp.id}`} className="btn btn-outline-primary">
                            <i className="bi bi-pencil"></i> Szerkesztés
                        </NavLink>
                    </div>
                </div>
            )}
        </div>
    );
};
