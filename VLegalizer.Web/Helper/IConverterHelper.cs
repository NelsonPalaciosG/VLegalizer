using VLegalizer.Common.Models;
using VLegalizer.Web.Data.Entities;

namespace VLegalizer.Web.Helper
{
    public interface IConverterHelper
    {
        TripResponse ToTripResponse(TripEntity tripEntity);

    }
}
