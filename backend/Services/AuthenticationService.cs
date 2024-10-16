using HospitalSystemAPI.DTOs.PatientDTOs;
using HospitalSystemAPI.DTOs;
using HospitalSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HospitalSystemAPI.Services
{
    public class AuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdGenerator _idGenerator;
        private readonly JWT _jwt;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IIdGenerator idGenerator, JWT jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _idGenerator = idGenerator;
            _jwt = jwt;
        }

        public async Task<AuthenticationObject> RegisterDoctorAsync(DoctorInsertionDTO dto)
        {
            // Check if the username or email already exists
            var validationResponse = await ValidateUserAsync(dto.UserName, dto.Email);
            if (!string.IsNullOrEmpty(validationResponse))
                return new AuthenticationObject() { Message = validationResponse };

            // Generate unique Doctor ID
            var doctorId = _idGenerator.GenerateDoctorId();

            // Create Doctor user
            var doctor = new Doctor
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                SpecialityId = dto.SpecialityId,
                Id = doctorId
            };

            return await RegisterUserAsync(doctor, dto.Password, "Doctor");
        }

        public async Task<AuthenticationObject> RegisterPatientAsync(PatientInsertionDTO dto)
        {
            // Check if the username or email already exists
            var validationResponse = await ValidateUserAsync(dto.UserName, dto.Email);
            if (!string.IsNullOrEmpty(validationResponse))
                return new AuthenticationObject() { Message = validationResponse };

            // Generate unique Patient ID
            var patientId = _idGenerator.GeneratePatientId();

            // Create Patient user
            var patient = new Patient
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate,
                Id = patientId
            };

            return await RegisterUserAsync(patient, dto.Password, "Patient");
        }

        private async Task<string> ValidateUserAsync(string userName, string email)
        {
            // Check if the username already exists
            var existingUserByName = await _userManager.FindByNameAsync(userName);
            if (existingUserByName is not null)
            {
                return "Username already exists.";
            }

            // Check if the email already exists
            var existingUserByEmail = await _userManager.FindByEmailAsync(email);
            if (existingUserByEmail is not null)
            {
                return "Email already exists.";
            }

            return string.Empty;
        }

        private async Task<AuthenticationObject> RegisterUserAsync(ApplicationUser user, string password, string role)
        {
            // Create the user with password
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthenticationObject { Message = errors };
            }

            // Assign role to the user (Doctor or Patient)
            await _userManager.AddToRoleAsync(user, role);

            // Create JWT token for the user
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthenticationObject
            {
                IsAuthenticated = true,
                UserName = user.UserName,
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                Role = role,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        // JWT Token creation logic
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("userId", user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }

}
