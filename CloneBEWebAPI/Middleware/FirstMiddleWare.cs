namespace CloneBEWebAPI.Middleware
{
    public class FirstMiddleWare
    {
        private readonly RequestDelegate requestDelegate;

        public FirstMiddleWare( RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate; // trỏ đến middlewrae phía sau first middleware 
        }
        //httpcontext đi qua middleware trong pipleline
        public async Task Invoke(HttpContext context)
        {

            Console.WriteLine(context.Request.Path); // trả vê cho mình 
           await context.Response.WriteAsync("tra ve cho client ");
            await requestDelegate(context); // nếu ko có cái này thì ko chuyển đếnb middleware phía sau được thì middlerware này coi là terminal middleware 
        }
    }
}
