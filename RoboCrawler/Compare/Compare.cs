public class Compare
{
    public static int comparePrice(string Mercado , string Magazine, int id)
    {

        char[] arg = { 'R', '$', ' ' }; 
        Mercado = Mercado.Trim(arg);
        Magazine = Magazine.Trim(arg);
        double MercadoPrecoComparado = Convert.ToDouble(Mercado);
        double MagazinePrecoComparado = Convert.ToDouble(Magazine);

        if ( MercadoPrecoComparado < MagazinePrecoComparado)
        {
            BenchmarkLog("0001", "Pedro", DateTime.Now, "Benchmarking", "Sucesso", id);
            return 1;
            
        } 
        else if (MercadoPrecoComparado > MagazinePrecoComparado) 
        {
            BenchmarkLog("0001", "Pedro", DateTime.Now, "Benchmarking", "Sucesso", id);
            return 2;
        }
        else
        {
            BenchmarkLog("0001", "Pedro", DateTime.Now, "Benchmarking", "Alerta", id);
            return 3;
        }
    }
    static void BenchmarkLog(string CodRobot, string UserName, DateTime logDate, string StateDescription, string ResultFeedBack, int ProductID)
    {
        using (var context = new CrawlerContext())
        {
            var log = new Log
            { CodRobot = CodRobot, UserName = UserName, LogDate = logDate, StateDescription = StateDescription, ResultFeedBack = ResultFeedBack, ProductID = ProductID };
            context.Logs.Add(log);
            context.SaveChanges();
        }
    }
}
