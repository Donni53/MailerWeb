using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailerWeb.Models;
using MailerWeb.Models.Repository;

namespace MailerWeb.Services
{
    public class AuthService
    {
        private readonly IUserRepository<User> _dataRepository;

        public AuthService(IUserRepository<User> dataRepository)
        {
            _dataRepository = dataRepository;
        }


        public async Task Auth(User user)
        {




            var dbUser = _dataRepository.GetByLogin(user.Login);
            if (dbUser == null)
            {
                //Проверка imap
                    //OK
                        //Добавление нового пользователя
                        //Генерация и возвращение токена
                    //Er
                        //Ошибка imap авторизации
            }
            else
            {
                //Сверка паролей
                    //Ok
                        //Проверка imap
                            //OK
                                //Генерация и возвращение токена
                            //Er
                                //Ошибка imap авторизации
                    //Er
                        //Проверка imap с новым паролем
                        
            }
        }


    }
}
