﻿using Green_eyes_server.Model;
using MongoDB.Driver;
using System.Collections.Generic;

namespace aspnet_core_dotnet_core.Services
{
    public abstract class PadraoServiceMongo<T> where T : PadraoModel
    {

        protected PadraoServiceMongo()
        {
            SetCollection();
        }

        protected abstract void SetCollection();

        protected string Collection { get; set; }

        public virtual void Insert(T model)
        {
            MongoDBConn conexao = new MongoDBConn();

            var collection = conexao.GetDatabase(conexao.GetConexao()).GetCollection<T>(Collection);

            collection.InsertOne(model);
        }

        public virtual void Update(T model)
        {
            //HelperDAO.ExecutaProc("spUpdate_" + Tabela, CriaParametros(model));
        }

        public virtual void Delete(int id)
        {
            /* var p = new SqlParameter[]
            {
                 new SqlParameter("id", id),
                 new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);*/
        }

        public virtual T Consulta(int id)
        {
            /*var p = new SqlParameter[]
            {
                new SqlParameter("id", id),
                new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaModel(tabela.Rows[0]);*/

            return null;
        }

        public virtual List<T> Listagem()
        {
            /*var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela),
                new SqlParameter("Ordem", "1") // 1 é o primeiro campo da tabela,
                                // ou seja, a chave primária
            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }*/
            return null;
        }






    }
}
