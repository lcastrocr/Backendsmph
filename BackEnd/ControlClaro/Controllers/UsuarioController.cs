﻿using Business.Models;
using Business.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Business.Utilities.Functions;

namespace ControlClaro.Controllers
{
    public class UsuarioController : ApiController
    {
        #region Definition of Services
        [HttpGet]
        [Route("api/usuario/lista/{pais_Id}/{emp_Id}/{criterio}")]
        public HttpResponseMessage Lista(int pais_Id, int emp_Id, string criterio)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                if (criterio == "_TODO_")
                    criterio = "";

                using (UsuarioService service = new UsuarioService())
                {
                    var usuarios = service.Lista(pais_Id, emp_Id, criterio);
                    data.result = new { usuarios, service.cantidad };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de usuarios");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        
        [HttpDelete]
        [Route("api/usuario/eliminar/{usuario_Id}/{emp_Id}/{producto_Id}")]
        public HttpResponseMessage Eliminar(string usuario_Id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.Eliminar(usuario_Id);
                    data.result = null;
                    data.status = true;
                    data.message = "El usuario seleccionado se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar usuario");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/usuario/change-password/{newPassword}/{currentpassword}")]
        public HttpResponseMessage ChangePassword(string newPassword,string currentpassword)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.ChangePassword(config.usuario.usuario_Id, newPassword, currentpassword);
                    data.result = null;
                    data.status = true;
                    data.message = "El cambio de contraseña se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Cambio de contraseña");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/usuario/changepasswordlogout/{username}/{currentpassword}")]
        public HttpResponseMessage ChangePasswordlogout(string currentpassword, string username)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                

                using (UsuarioService service = new UsuarioService())
                {
                    service.ChangePasswordlogout(username, currentpassword);
                    data.result = null;
                    data.status = true;
                    data.message = "El cambio de contraseña se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Cambio de contraseña");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }






        [HttpPost]
        [Route("api/user/savephoto/")]
        public HttpResponseMessage savePhoto([FromBody] Usuario user)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.savephoto(user);
                    data.result = null;
                    data.status = true;
                    data.message = "El cambio de foto se completó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Cambio de foto");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }






        [HttpGet]
        [Route("api/usuario/roles")]
        public HttpResponseMessage ListRole()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    var roles = service.ListRole();
                    data.result = new { roles };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de Roles");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }




        [HttpGet]
        [Route("api/usuario/admi/{filter}")]
        public HttpResponseMessage ListallUsers(string filter)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                if (filter == "_ALL_")
                {
                    filter = "";
                }
                using (UsuarioService service = new UsuarioService())
                {
                    var users = service.UsersWithAll(filter);
                    data.result = new { users };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Lista de usuarios");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }



        [HttpDelete]
        [Route("api/user/delete/{userid}")]
        public HttpResponseMessage DeleteUser(int userid)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    service.DeleteUser(userid);
                    data.result = null;
                    data.status = true;
                    data.message = "Se ha eliminado el usuario";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Eliminar Usuario");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }
        [HttpGet]
        [Route("api/usuario/PersonbyId/{Person_Id}")]
        public HttpResponseMessage PersonbyId(int Person_Id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();

            try
            {
                VerifyMessage(config.errorMessage);

                using (UsuarioService service = new UsuarioService())
                {
                    var Persona = service.PersonbyId(Person_Id);
                    data.result = new { Persona };
                    data.status = true;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Persona Encontrada");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }

        [HttpPost]
        [Route("api/usuario/save")]
        public HttpResponseMessage Guardar([FromBody] Usuario usuario)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResponseConfig config = VerifyAuthorization(Request.Headers);
            RestResponse data = new RestResponse();


            try
            {
                VerifyMessage(config.errorMessage);
                using (UsuarioService service = new UsuarioService())
                {
                    service.save(usuario);
                    data.result = null;
                    data.status = true;
                    data.message = usuario.usuario_Id == "-1" ? " Se creó correctamente el funcionario " :" Se actualizó correctamente el funcionario" ;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = config.isAuthenticated ? HttpStatusCode.BadRequest : HttpStatusCode.Unauthorized;
                data.status = false;
                data.message = ex.Message;
                data.error = NewError(ex, "Registro");
            }
            finally
            {
                response.Content = CreateContent(data);
            }

            return response;
        }



        #endregion
    }
}