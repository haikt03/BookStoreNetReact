using Microsoft.AspNetCore.Identity;

namespace BookStoreNetReact.Infrastructure.Identity
{
    public class VietnameseIdentityErrorDescriber: IdentityErrorDescriber
    {
        public override IdentityError DefaultError() =>
        new IdentityError { Code = nameof(DefaultError), Description = "Lỗi không xác định." };

        public override IdentityError ConcurrencyFailure() =>
            new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Lỗi xung đột dữ liệu." };

        public override IdentityError PasswordMismatch() =>
            new IdentityError { Code = nameof(PasswordMismatch), Description = "Mật khẩu không chính xác." };

        public override IdentityError InvalidToken() =>
            new IdentityError { Code = nameof(InvalidToken), Description = "Mã token không hợp lệ." };

        public override IdentityError LoginAlreadyAssociated() =>
            new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Tài khoản đã được liên kết với người dùng khác." };

        public override IdentityError InvalidUserName(string? userName) =>
            new IdentityError { Code = nameof(InvalidUserName), Description = $"Tên người dùng '{userName}' không hợp lệ." };

        public override IdentityError InvalidEmail(string? email) =>
            new IdentityError { Code = nameof(InvalidEmail), Description = $"Email '{email}' không hợp lệ." };

        public override IdentityError DuplicateUserName(string userName) =>
            new IdentityError { Code = nameof(DuplicateUserName), Description = $"Tên người dùng '{userName}' đã tồn tại." };

        public override IdentityError DuplicateEmail(string email) =>
            new IdentityError { Code = nameof(DuplicateEmail), Description = $"Email '{email}' đã tồn tại." };

        public override IdentityError InvalidRoleName(string? role) =>
            new IdentityError { Code = nameof(InvalidRoleName), Description = $"Tên vai trò '{role}' không hợp lệ." };

        public override IdentityError DuplicateRoleName(string role) =>
            new IdentityError { Code = nameof(DuplicateRoleName), Description = $"Tên vai trò '{role}' đã tồn tại." };

        public override IdentityError UserAlreadyHasPassword() =>
            new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Người dùng đã có mật khẩu." };

        public override IdentityError UserLockoutNotEnabled() =>
            new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Tính năng khoá tài khoản chưa được bật cho người dùng này." };

        public override IdentityError UserAlreadyInRole(string role) =>
            new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Người dùng đã thuộc vai trò '{role}'." };

        public override IdentityError UserNotInRole(string role) =>
            new IdentityError { Code = nameof(UserNotInRole), Description = $"Người dùng không thuộc vai trò '{role}'." };

        public override IdentityError PasswordTooShort(int length) =>
            new IdentityError { Code = nameof(PasswordTooShort), Description = $"Mật khẩu phải có ít nhất {length} ký tự." };

        public override IdentityError PasswordRequiresNonAlphanumeric() =>
            new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Mật khẩu phải có ít nhất một ký tự đặc biệt." };

        public override IdentityError PasswordRequiresDigit() =>
            new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Mật khẩu phải có ít nhất một chữ số (0-9)." };

        public override IdentityError PasswordRequiresLower() =>
            new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Mật khẩu phải có ít nhất một chữ cái thường (a-z)." };

        public override IdentityError PasswordRequiresUpper() =>
            new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Mật khẩu phải có ít nhất một chữ cái hoa (A-Z)." };
    }
}
