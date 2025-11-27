using System;
using System.Collections.Generic;
using System.Text;

using MMABooksTools;
using MMABooksProps;

using System.Data;

// *** I use an "alias" for the ado.net classes throughout my code
// When I switch to an oracle database, I ONLY have to change the actual classes here
using DBBase = MMABooksTools.BaseSQLDB;
using DBConnection = MySql.Data.MySqlClient.MySqlConnection;
using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using DBParameter = MySql.Data.MySqlClient.MySqlParameter;
using DBDataReader = MySql.Data.MySqlClient.MySqlDataReader;
using DBDataAdapter = MySql.Data.MySqlClient.MySqlDataAdapter;
using DBDbType = MySql.Data.MySqlClient.MySqlDbType;

namespace MMABooksDB;

public class ProductDB : DBBase, IReadDB, IWriteDB
{
    public IBaseProps Create(IBaseProps p)
    {

        int rowsAffected = 0;
        ProductProps props = (ProductProps)p;

        DBCommand command = new DBCommand();
        command.CommandText = "usp_ProductCreate";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("prodId", DBDbType.Int32);
        command.Parameters.Add("prodCode", DBDbType.VarChar);
        command.Parameters.Add("Description_p", DBDbType.VarChar);
        command.Parameters.Add("UnitPrice_p", DBDbType.Decimal);
        command.Parameters.Add("OnHandQuantity_p", DBDbType.Int32);


        command.Parameters[0].Direction = ParameterDirection.Output;
        command.Parameters["prodCode"].Value = props.ProductCode;
        command.Parameters["Description_p"].Value = props.Description;
        command.Parameters["UnitPrice_p"].Value = props.UnitPrice;
        command.Parameters["OnHandQuantity_p"].Value = props.OnHandQuantity;



        try
        {
            rowsAffected = RunNonQueryProcedure(command);
            if (rowsAffected == 1)
            {
                props.ProductID = (int)command.Parameters[0].Value;
                props.ConcurrencyID = 1;
                return props;
            }
            else
                throw new Exception("Unable to insert record. " + props.ToString());
        }
        catch (Exception e)
        {
            // log this error
            throw;
        }
        finally
        {
            if (mConnection.State == ConnectionState.Open)
                mConnection.Close();
        }
    }

    public bool Delete(IBaseProps props)
    {
        throw new NotImplementedException();
    }

    public IBaseProps Retrieve(object key)
    {
        DBDataReader data = null;
        ProductProps props = new ProductProps();
        DBCommand command = new DBCommand();

        command.CommandText = "usp_ProductSelect";
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add("prodCode", DBDbType.VarChar);
        command.Parameters["prodCode"].Value = (string)key;

        try
        {
            data = RunProcedure(command);

            if (!data.IsClosed)
            {
                if (data.Read())
                {
                    props.SetState(data);
                }
                else
                    throw new Exception("Record does not exist in the database.");
            }
            return props;
        }
        catch (Exception e)
        {
            // log this exception
            throw;
        }
        finally
        {
            if (data != null)
            {
                if (!data.IsClosed)
                    data.Close();
            }
        }
    }

    public object RetrieveAll()
    {
        throw new NotImplementedException();
    }

    public bool Update(IBaseProps props)
    {
        throw new NotImplementedException();
    }
}

