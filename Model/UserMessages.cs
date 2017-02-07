
using System;
using System.Collections.Generic;

namespace EscolaDeVoce.Frontend
{
    public class UserMessages{
        public List<Services.ViewModel.MessageViewModel> mensagens { get; set; }
        public Guid userid { get; set; }
    }
}