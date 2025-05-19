import axios from 'axios';
import { useNavigate } from "react-router-dom";

export const ItmpCreatePage = () => {
    const navigate = useNavigate();

    const handleSubmit = async (event) => {
        event.preventDefault();

        const name = event.target.elements.name.value;
        const email = event.target.elements.email.value;

        try {
            await axios.post("https://itmp.sulla.hu/users", {
                name,
                email,
            });
            navigate("/");
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <div className="p-5 content bg-whitesmoke text-center">
            <h2>Új ITMP bejegyzés</h2>
            <form onSubmit={handleSubmit}>
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">ITMP bejegyzés neve:</label>
                    <div className="col-sm-9">
                        <input type="text" name="name" className="form-control" required />
                    </div>
                </div>
                <div className="form-group row pb-3">
                    <label className="col-sm-3 col-form-label">Email cím:</label>
                    <div className="col-sm-9">
                        <input type="email" name="email" className="form-control" required />
                    </div>
                </div>

                <button type="submit" className="btn btn-success">
                    Küldés
                </button>
            </form>
        </div>
    );
};
