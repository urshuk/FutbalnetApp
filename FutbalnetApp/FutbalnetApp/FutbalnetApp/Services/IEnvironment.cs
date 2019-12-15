using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FutbalnetApp.Services
{
    public interface IEnvironment
    {
        Theme GetOperatingSystemTheme();
        Task<Theme> GetOperatingSystemThemeAsync();
    }

    public enum Theme { Light, Dark }
}
