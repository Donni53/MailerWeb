using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Utilities.IO;

namespace MailerWeb.Client.Services
{
    public class LanguageService
    {
        public List<Dictionary<string, string>> Languages { get; set; }

        public LanguageService()
        {
            var enLanguage = new Dictionary<string, string>();
            enLanguage.Add("accManager", "Accounts Manager");
            enLanguage.Add("entEmail", "Enter email");
            enLanguage.Add("wwNs", "We'll never share your email with anyone else.");
            enLanguage.Add("pass", "Password");
            enLanguage.Add("addSet", "Show Additional Settings");
            enLanguage.Add("entImap", "Enter Imap Address");
            enLanguage.Add("entImapPort", "Enter Imap Port");
            enLanguage.Add("entSmtp", "Enter Smtp Address");
            enLanguage.Add("entSmtpPort", "Enter Smtp Port");
            enLanguage.Add("updSett", "Update Connection Settings");
            enLanguage.Add("si", "Sign In");
            enLanguage.Add("curAcc", "Current account");
            enLanguage.Add("addedAccs", "Added accounts");
            enLanguage.Add("del", "Delete");
            enLanguage.Add("pw", "Please, wait..");
            enLanguage.Add("sa", "Successful authorization!");
            enLanguage.Add("from","From");
            enLanguage.Add("to", "To");
            enLanguage.Add("answ", "Answer");
            enLanguage.Add("ok", "Ok!");
            enLanguage.Add("mail", "Mail");
            enLanguage.Add("mailbox", "Mailbox");
            enLanguage.Add("crt", "Create");
            enLanguage.Add("fdname", "Folder Name");
            enLanguage.Add("unseen", "Unseen");
            enLanguage.Add("answed", "Answered");
            enLanguage.Add("imp", "Important");
            enLanguage.Add("lm", "Load More");
            enLanguage.Add("oops", "Oops!");
            enLanguage.Add("notlogged", "You are not logged in yet!");
            enLanguage.Add("ea", "Email address");
            enLanguage.Add("ss", "Select signature");
            enLanguage.Add("st", "Signature text");
            enLanguage.Add("subj", "Subject");
            enLanguage.Add("et", "Email text");
            enLanguage.Add("send", "Send");
            enLanguage.Add("ms", "Mailsender");
            enLanguage.Add("sett", "Settings");
            enLanguage.Add("name", "Name");
            enLanguage.Add("jd", "John Doe");
            enLanguage.Add("add", "Add");
            enLanguage.Add("edit", "Edit");
            enLanguage.Add("lang", "Language");
            enLanguage.Add("theme", "Theme");
            enLanguage.Add("prr", "Page reboot required");
            enLanguage.Add("save", "Save");


            var ruLanguage = new Dictionary<string, string>();
            ruLanguage.Add("accManager", "Менеджер аккаунтов");
            ruLanguage.Add("entEmail", "Введите почту");
            ruLanguage.Add("wwNs", "Мы ни с кем не поделимся вашей почтой.");
            ruLanguage.Add("pass", "Пароль");
            ruLanguage.Add("addSet", "Показать дополнительные настройки");
            ruLanguage.Add("entImap", "Введите Imap адрес");
            ruLanguage.Add("entImapPort", "Введите Imap порт");
            ruLanguage.Add("entSmtp", "Введите Smtp адрес");
            ruLanguage.Add("entSmtpPort", "Введите Smtp порт");
            ruLanguage.Add("updSett", "Обновить настройки подключения");
            ruLanguage.Add("si", "Войти");
            ruLanguage.Add("curAcc", "Текущий аккаунт");
            ruLanguage.Add("addedAccs", "Аккаунты");
            ruLanguage.Add("del", "Удалить");
            ruLanguage.Add("pw", "Пожалуйста, подождите...");
            ruLanguage.Add("sa", "Успешная авторизация!");
            ruLanguage.Add("from", "От");
            ruLanguage.Add("to", "Кому");
            ruLanguage.Add("answ", "Ответить");
            ruLanguage.Add("ok", "Четко!");
            ruLanguage.Add("mail", "Письмо");
            ruLanguage.Add("mailbox", "Почтовый ящик");
            ruLanguage.Add("crt", "Создать");
            ruLanguage.Add("fdname", "Имя папки");
            ruLanguage.Add("unseen", "Новое");
            ruLanguage.Add("answed", "Отвечено");
            ruLanguage.Add("imp", "Важно");
            ruLanguage.Add("lm", "Загрузить больше");
            ruLanguage.Add("oops", "Упсс!");
            ruLanguage.Add("notlogged", "Вы еще не авторизованы!");
            ruLanguage.Add("ea", "Адрес");
            ruLanguage.Add("ss", "Выберите подпись");
            ruLanguage.Add("st", "Выберите подпись");
            ruLanguage.Add("subj", "Тема");
            ruLanguage.Add("et", "Текст письма");
            ruLanguage.Add("send", "Отправить");
            ruLanguage.Add("ms", "Отправка");
            ruLanguage.Add("sett", "Настройки");
            ruLanguage.Add("name", "Имя");
            ruLanguage.Add("jd", "Джон Доу");
            ruLanguage.Add("add", "Добавить");
            ruLanguage.Add("edit", "Изменить");
            ruLanguage.Add("lang", "Язык");
            ruLanguage.Add("theme", "Тема");
            ruLanguage.Add("prr", "Необходима перезагрузка страницы");
            ruLanguage.Add("save", "Сохранить");

            Languages = new List<Dictionary<string, string>>{ enLanguage, ruLanguage };
        }
    }
}
