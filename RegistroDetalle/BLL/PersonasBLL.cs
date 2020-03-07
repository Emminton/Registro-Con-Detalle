using Microsoft.EntityFrameworkCore;
using RegistroDetalle.DAL;
using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RegistroDetalle.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Persona.Add(personas) != null)
                    paso = (db.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;

        }

        public static bool Modificar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                ////buscar las entidades que no estan para removerlas
                var Anterior = PersonasBLL.Buscar(personas.PersonaId);
                foreach (var item in Anterior.Telefonos)
                {
                    if (!personas.Telefonos.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;

                }
                //recorrer el detalle
                foreach (var item in personas.Telefonos)
                {
                    //Muy importante indicar que pasara con la entidad del detalle
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }
                ////Idicar que se esta modificando el encabezado
                db.Entry(personas).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var eliminar = db.Persona.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar(int id)
        {
            Personas personas = new Personas();
            Contexto db = new Contexto();

            try
            {
                personas = db.Persona.Where(o => o.PersonaId == id).Include(o => o.Telefonos).SingleOrDefault();
                //// if (personas != null)
                //personas = db.Persona.Find(id);
                //// if (personas != null)
                //personas.Telefonos.Count();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return personas;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> personas)
        {
            List<Personas> lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                lista = db.Persona.Where(personas).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return lista;
        }
        
    }
}
       
