using Newtonsoft.Json;
using SportSparkCoreSharedLibrary.DTOs;
using System.ComponentModel;

namespace SportSparkWeb.Models;

public class UserData : INotifyPropertyChanged
{
    private UserDTO userDTO;
    public UserDTO UserDTO
    {
        get { return userDTO; }
        set
        {
            if (userDTO != value)
            {
                userDTO = value;
                OnPropertyChanged(nameof(UserDTO));
            }
        }
    }
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public void UpdateUserData(UserDTO userDTO)
    {
        UserDTO = userDTO;
    }
}
