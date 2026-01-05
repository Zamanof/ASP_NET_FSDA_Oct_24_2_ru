namespace ASP_NET_01._CoR.Concrete;

class CheckDirector
{
    public bool MakeUserChecker(User user)
    {
        UserNameChecker userNameChecker = new UserNameChecker();
        PasswordChecker passwordChecker = new PasswordChecker();
        EmailChecker emailChecker = new EmailChecker();
        userNameChecker.Next = passwordChecker;
        passwordChecker.Next = emailChecker;
        return userNameChecker.Check(user);
    }
}
