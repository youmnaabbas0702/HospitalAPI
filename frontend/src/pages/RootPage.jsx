import { Outlet } from "react-router";
import NaVBar from "../components/NavBar";
function RootPage(){
return(
    <>
    <NaVBar/>
    <Outlet/>
    </>
);
}
export default RootPage;