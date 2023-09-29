using System;
namespace Obligatorio_Dominio
{
	public class Sistema
	{
        public List<Miembro> _miembros = new List<Miembro>();
        public List<Administrador> _administradores = new List<Administrador>();
        private List<Invitacion> _invitaciones = new List<Invitacion>();
        private List<Publicacion> _publicaciones = new List<Publicacion>();
        private List<Reaccion> _reacciones = new List<Reaccion>();


        public Sistema()
		{
            PrecargaMiembros();
            PrecargaAdm();
            PrecargaInvitaciones();
            PrecargaPostyComentarios();
            PrecargaReacciones();
        }

        public void Precargas()
		{
			PrecargaMiembros();
            PrecargaAdm();
            PrecargaInvitaciones();
			PrecargaPostyComentarios();
			PrecargaReacciones();
		}

        private void PrecargaReacciones()
        {
            throw new NotImplementedException();
        }

        private void PrecargaPostyComentarios()
        {
            throw new NotImplementedException();
        }

        private void PrecargaInvitaciones()
        {
            throw new NotImplementedException();
        }

        private void PrecargaAdm()
        {
            Administrador unAdministrador = new Administrador("esteban@gmail.com", "contrasena123");
            AltaAdministrador(unAdministrador);
        }


        public void AltaAdministrador(Administrador administrador)
        {
            if (administrador == null)
            {
                throw new Exception("El administrador no es válido.");
            }
            if (_administradores.Contains(administrador))
            {
                throw new Exception($"El administrador {administrador.Mail} ya existe.");
            }
            administrador.Validar();
            _administradores.Add(administrador);
        }

        private void PrecargaMiembros()
        {
            Miembro unMiembro1 = new Miembro("esteban@gmail.com", "contrasena123", "Esteban", "Recayte", new DateTime(1995, 05, 04), false);
            AltaMiembro(unMiembro1);

            Miembro unMiembro2 = new Miembro("mateo@gmail.com", "mateo1234", "Mateo", "Rodriguez", new DateTime(1997, 02, 01), true);
            AltaMiembro(unMiembro2);

            Miembro unMiembro3 = new Miembro("carlos@gmail.com", "carlos1234", "Carlos", "Lopez", new DateTime(1983, 07, 10), true);
            AltaMiembro(unMiembro3);

            Miembro unMiembro4 = new Miembro("laura@gmail.com", "laura1234", "Laura", "Martinez", new DateTime(1995, 12, 03), false);
            AltaMiembro(unMiembro4);

            Miembro unMiembro5 = new Miembro("maria@gmail.com", "maria1234", "Maria", "Sanchez", new DateTime(1987, 04, 18), false);
            AltaMiembro(unMiembro5);

            Miembro unMiembro6 = new Miembro("pedro@gmail.com", "pedro1234", "Pedro", "Gomez", new DateTime(1992, 06, 25), true);
            AltaMiembro(unMiembro6);

            Miembro unMiembro7 = new Miembro("luis@gmail.com", "luis1234", "Luis", "Fernandez", new DateTime(1988, 11, 07), false);
            AltaMiembro(unMiembro7);

            Miembro unMiembro8 = new Miembro("sara@gmail.com", "sara1234", "Sara", "Ramirez", new DateTime(1994, 03, 29), false);
            AltaMiembro(unMiembro8);

            Miembro unMiembro9 = new Miembro("andres@gmail.com", "andres1234", "Andres", "Perez", new DateTime(1996, 09, 14), false);
            AltaMiembro(unMiembro9);

            Miembro unMiembro10 = new Miembro("lucas@gmail.com", "lucas1234", "Lucas", "Lopez", new DateTime(1991, 01, 08), true);
            AltaMiembro(unMiembro10);
        }


        public void AltaMiembro(Miembro miembro)
        {
            if (miembro == null)
            {
                throw new Exception("El miembro no es válido.");
            }
            if (_miembros.Contains(miembro))
            {
                throw new Exception($"El miembro {miembro.Mail} ya existe.");
            }
            miembro.Validar();
            _miembros.Add(miembro);
            // validar con equals
        }
    }
}

