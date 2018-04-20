using Services.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Memory;
using System.Web.Mvc;

namespace Services.Business
{
    public class PlayBusiness : Singleton<PlayBusiness>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Singleton"/> class.
        /// </summary>
        private PlayBusiness() { }

        public sp_Random_Result Case()
        {
            var _entity = Connection.Default.sp_Random().FirstOrDefault();            
            return _entity;
        }

        public List<string> Valid(int suspect, int place, int weapon)
        {
            var _result = new List<string>();
            var _message = string.Empty;
            var _suspect = false;
            var _place = false;
            var _weapon = false;

            sp_Random_Result _crime = (sp_Random_Result)Services.Business.PlayBusiness.Instance.Crime;

            _suspect = (_crime.Suspects == suspect);
            _place = (_crime.Places == place);
            _weapon = (_crime.Weapons == weapon);

            if (_suspect && _place && _weapon)
            {
                _result.Add("true");
                _result.Add("Parabéns! Você solucionou o caso!<br /><br /><button type='button' id='btnConfirm' class='btn btn-info btn-sm'>Novo Jogo</button>&nbsp;&nbsp;&nbsp;&nbsp;<button type='button' id='btnQuit' class='btn btn-warning btn-sm'>Fechar</button>");
            }
            else if (!_suspect && !_place && !_weapon)
            {
                _result.Add("false");
                _result.Add("Todos estão incorretos!");
            } else if (!_suspect && (_place || _weapon)) {
                _result.Add("false");
                _result.Add("Assassino está incorreto!");
            } else if (!_place && (_suspect || _weapon)) {
                _result.Add("false");
                _result.Add("Local está incorreto!");
            }
            else if (!_weapon && (_suspect || _place)) {
                _result.Add("false");
                _result.Add("Arma está incorreta!");
            } else {
                _result.Add("false");
                _result.Add("Teoria incorreta!");
            }
            return _result;
        }

        public object Crime
        {
            get { return InSession.Entity<object>("Crime"); }
            set { InSession.Entity<object>("Crime", this.Case(), true); }
        }

    }
}
