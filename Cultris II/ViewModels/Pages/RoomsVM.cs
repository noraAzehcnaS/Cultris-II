using Cultris_II.Models.C2API;
using Cultris_II.Services;
using Cultris_II.ViewModels.Base;
using Cultris_II.Views.Modals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cultris_II.ViewModels.Pages
{
    public class RoomsVM : BaseVM
    {
        private Session session;
        public ObservableCollection<Room> Rooms { get; }
        public Command LoadRoomsCommand { get; }
        public Command<Room> RoomTappedCommand { get; }
        public RoomsVM()
        {

            Rooms = new ObservableCollection<Room>();
            LoadRoomsCommand = new Command(async () => await LoadRooms());
            RoomTappedCommand = new Command<Room>(OnRoomSelected);
        }

        public async Task LoadRooms()
        {
            Rooms.Clear();
            session = await C2API_Service.GetSession();
            if(session != null && session.Rooms != null)
            {
                foreach (Room room in session.Rooms)
                {
                    SetImageSourceForRoom(room);
                    Rooms.Add(room);
                }
            }
            IsBusy = false;
        }

        private void SetImageSourceForRoom(Room room)
        {
            switch (room.Mode) 
            {
                case "Standard":
                    room.ImageSource = "standard_room.png";
                    break;
                case "Swiss cheese":
                    room.ImageSource = "swiss_cheese_room.png";
                    break;
                default:
                    room.ImageSource = "standard_room.png";
                    break;
            }
            if (room.Teamplay)
            {
                room.ImageSource = "teams_room.png";
            }
        }

        private void OnRoomSelected(Room room)
        {
            if (room.Players > 0) 
            {
                Navigation().PushModalAsync(new RoomModal(PlayersInRoom(room.Id)));
            }
        }

        private List<Player> PlayersInRoom(int roomId)
        {
            return session.Players.Where(p => p.Id.Equals(roomId)).ToList();
        }
    }
}
