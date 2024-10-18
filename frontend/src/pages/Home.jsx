import { useSelector } from "react-redux";
import AdminHome from "../components/admin/AdminHome";

function Home(){
const role = useSelector((state) => state.auth.role);

console.log(role);
    return (
        <>
        {role === "administrator" ? <AdminHome /> : <h1>User</h1>}
   </>
   );
}
export default Home