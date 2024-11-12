using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Requests.BarberShop;
using Bogus;

namespace Common.Tests.Utilities.Requests.BarberShops;

public class RequestCreateOpeningHourJsonBuilder
{
    public static RequestCreateOpeningHourJson Build(List<RequestDateJson> openingHours, DateTime? startDate = null, DateTime? endDate = null, int? quantityHour = null)
    {
        return new RequestCreateOpeningHourJson 
        {
            OpeningHours = openingHours,
        };            
    }

    public static List<RequestDateJson> Collection(uint count = 2, DateTime? startDate = null, DateTime? endDate = null)
    {
        var list = new List<RequestDateJson>();

        if(count == 0)
            count = 1;

        for(int i = 0; i < count; i++)
        {
            list.Add(RequestDateJsonBuilder.Build(startDate, endDate));
        }    

        return list;
    }
}
