using ASP_NET_01._CoR;
using ASP_NET_01._CoR.Concrete;

User user = new User("mr.13", "Salam123456", "zamanov@itstep.org");
var director = new CheckDirector(); 
Console.WriteLine(director.MakeUserChecker(user));
