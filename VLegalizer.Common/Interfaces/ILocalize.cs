using System.Globalization;

namespace VLegalizer.Common.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }

}
