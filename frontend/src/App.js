
import RootPage from './pages/RootPage';
import Home from './pages/Home';
import Login from './components/register/Login';
import Register from './components/register/Register';
import './App.css';
import PrivateRoute from './components/PrivateRoute';
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import MangeDoctors from './components/admin/MangeDoctors';
import MangePatients from './components/admin/MangePatients';
import MangeAppoints from './components/admin/ManageAppoints';
import ManageSpecial from './components/admin/ManageSpecial';
const router = createBrowserRouter([{
  path: "/",
  element: <RootPage/>,
  children:[
    {
      path:"/",
      element:(
      <PrivateRoute><Home/></PrivateRoute>
      )
    },
    {
      path:"login",
      element:<Login/>
    },
    {
    path: "register",
    element:<Register/>
    },
    {
      path:"mdoctors",
      element:(
        <PrivateRoute>
      <MangeDoctors/>
      </PrivateRoute>
    )
    },
    {
      path:"mpatients",
      element:(
      <PrivateRoute><MangePatients/></PrivateRoute>
      )
    },
    {
    path:"mappoints",
    element:(
      <PrivateRoute>
        <MangeAppoints/>
      </PrivateRoute>
    )
  },
  {
    path:"mspecialities",
    element:(
      <PrivateRoute>
        <ManageSpecial/>
      </PrivateRoute>
    )
  }
  ]
}])



function App() {
  return (
    <div className="App">
    <RouterProvider router={router} />
    </div>
  );
}

export default App;
