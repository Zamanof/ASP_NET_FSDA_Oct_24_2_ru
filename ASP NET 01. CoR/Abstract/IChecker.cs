namespace ASP_NET_01._CoR.Abstract;

interface IChecker
{
    public IChecker Next { get; set; }
    public bool Check(object request);
}
