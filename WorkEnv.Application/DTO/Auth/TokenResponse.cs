namespace WorkEnv.Application.DTO.Auth;

public record TokenResponse(
    string accessToken,
    string refreshToken,
    DateTime refreshTokenExpiresAt
);