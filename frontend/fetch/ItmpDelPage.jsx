import React, { useState, useEffect } from 'react';
import { useParams, useNavigate, NavLink } from 'react-router-dom';

export const ItmpDelPage = () => {
    const { ItmpId: id } = useParams();
    const navigate = useNavigate();
    const [itmp, setItmp] = useState(null);
    const [isPending, setPending] = useState(false);

    useEffect(() => {
        setPending(true);
        fetch(`https://itmp.sulla.hu/users/${id}`)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(`HTTP error! Status: ${res.status}`);
                }
                return res.json();
            })
            .then((data) => setItmp(data))
            .catch((error) => console.error(error))
            .finally(() => setPending(false));
    }, [id]);

    const handleDelete = async (event) => {
        event.preventDefault();
        try {
            const response = await fetch(`https://itmp.sulla.hu/users/${id}`, {
                method: 'DELETE',
            });
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            navigate("/");
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="p-5 m-auto text-center content bg-lavender">
            {isPending || !itmp ? (
                <div className="spinner-border"></div>
            ) : (
                <div className="card p-3">
                    <div className="card-body">
                        <h5 className="card-title">Törlendő elem: {itmp.name}</h5>
                        <div className="lead">E-mail: {itmp.email}</div>
                    </div>
                    <form onSubmit={handleDelete}>
                        <div>
                            <NavLink to={"/"}>
                                <button type="button" className="btn btn-secondary bi bi-backspace">
                                    &nbsp;Mégsem
                                </button>
                            </NavLink>
                            &nbsp;&nbsp;
                            <button type="submit" className="btn btn-danger bi bi-trash3">
                                &nbsp;Törlés
                            </button>
                        </div>
                    </form>
                </div>
            )}
        </div>
    );
};
