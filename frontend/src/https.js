export async function register(userData){
    const response = await fetch("http://localhost:8000/users",{
        method:"POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(userData),
    });
    if (!response.ok) {
        throw new Error("Registration failed");
      }
      return response.json();
}
// export async function login(credentials) {
//     const response = await fetch("http://localhost:5000/api/login", {
//       method: "POST",
//       headers: {
//         "Content-Type": "application/json",
//       },
//       body: JSON.stringify(credentials),
//     });
  
//     if (!response.ok) {
//       throw new Error("Login failed");
//     }
  
//     return response.json();
//   }
 export async function login({ email, password, role }) {
    try {
        const response = await fetch('http://localhost:8000/users');
        const users = await response.json();
        console.log(users);
        console.log("usera");
        const user = users.find(user => 
            user.email === email && 
            user.password === password && 
            user.role === role
        );
        console.log(user);

        if (user) {
            return {
                success: true,
                access_token: 'dummy_access_token', // Replace with actual token generation logic
                refresh_token: 'dummy_refresh_token', // Replace with actual token generation logic
                role: user.role
            };
        } else {
            return {
                success: false,
                message: 'Invalid credentials'
            };
        }
    } catch (error) {
        return {
            success: false,
            message: error.message
        };
    }
};
const fetchWithAuth = async (url, options = {}) => {
    // Add Authorization header
    const token = localStorage.getItem("token");
    const headers = {
        ...options.headers,
        "Authorization": `Bearer ${token}`,
        "Content-Type": "application/json"
    };

    try {
        const response = await fetch(url, {
            ...options,
            headers
        });

        // Check if the response status indicates an error
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        return response; // Return the full response object
    } catch (error) {
        console.error("Error during fetch:", error);
        throw error;
    }
};
