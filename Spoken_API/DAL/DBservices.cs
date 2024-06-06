using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Spoken_API.BL;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    //-------------------------------------------------------------------------------------
    //Users
    //-------------------------------------------------------------------------------------
    public int InsertUser(Users user)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertUserWithStoredProcedure("SP_insertUserToTbl", con, user);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
         
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertUserWithStoredProcedure(String spName, SqlConnection con, Users user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@userName", user.UserName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@phone", user.Phone);
        cmd.Parameters.AddWithValue("@LangName", user.LangName);
        cmd.Parameters.AddWithValue("@domainName", user.DomainName);
        cmd.Parameters.AddWithValue("@job", user.Job);
        cmd.Parameters.AddWithValue("@employee", user.Employee);
        cmd.Parameters.AddWithValue("@signature", user.Signature);


        return cmd;
    }

    public List<Users> ReadUsers()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Users> usersList = new List<Users>();

        cmd = buildReadStoredProcedureCommand(con, "SP_readAllUsers");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Users u = new Users();

            u.UserName = dataReader["userName"].ToString();
            u.Email = dataReader["email"].ToString();
            u.Password = dataReader["password"].ToString();
            u.Phone = dataReader["phone"].ToString();
            u.LangName = dataReader["LangName"].ToString();
            u.DomainName = dataReader["domainName"].ToString();
            u.Job = dataReader["job"].ToString();
            u.Employee = Convert.ToBoolean(dataReader["employee"]);
            u.Signature = dataReader["signature"].ToString(); 
            usersList.Add(u);
        }

        if (con != null)
        {
            con.Close();
        }

        return usersList;

    }


    SqlCommand buildReadStoredProcedureCommand(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }

    public Users LoginU(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }


        cmd = buildLogInStoredProcedureCommand(con, "SP_login", email, password);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dataReader == null)
        {
            return null;
        }
        Users u1 = new Users();
        while (dataReader.Read())
        {

            u1.UserName = dataReader["userName"].ToString();
            u1.Email = dataReader["email"].ToString();
            u1.Password = dataReader["password"].ToString();
            u1.Phone = dataReader["phone"].ToString();
            u1.LangName = dataReader["LangName"].ToString();
            u1.DomainName = dataReader["domainName"].ToString();
            u1.Job = dataReader["job"].ToString();
            u1.Employee = Convert.ToBoolean(dataReader["employee"]);
            u1.Signature = dataReader["signature"].ToString();
        }

        if (con != null)
        {
            con.Close();
        }

        return u1;

    }

    SqlCommand buildLogInStoredProcedureCommand(SqlConnection con, string spName, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);
        return cmd;

    }




    //-------------------------------------------------------------------------------------
    //Templates
    //-------------------------------------------------------------------------------------
    public List<Templates> ReadTemplate()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // Create the database connection
        }
        catch (Exception ex)
        {
            //  Handle the exception or log it
            throw (ex);
        }

        List<Templates> templatesList = new List<Templates>();

        cmd = buildReadStoredProcedureCommandTemplate(con, "SP_readAllTemplates");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Templates template = new Templates();

            //  Populate the template object with data from the data reader
            template.TemplateNo = dataReader["TemplateNo"].ToString();
            template.TemplateName = dataReader["TemplateName"].ToString();
            template.Description = dataReader["Description"].ToString();
            template.Email = dataReader["CreatorEmail"].ToString(); // Update column name to match database
            template.IsPublic = Convert.ToBoolean(dataReader["IsPublic"]); // Assuming IsPublic is a boolean in the database

            templatesList.Add(template);
        }

        if (con != null)
        {
            con.Close();
        }

        return templatesList;
    }


    SqlCommand buildReadStoredProcedureCommandTemplate(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }

    //read template by user

    public List<Templates> ReadTemplateByUser(string email)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Templates> templatesList = new List<Templates>();

        cmd = buildReadStoredProcedureCommandTemplateByUser(con, "SP_getTemplatesByEmail", email);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dataReader == null)
        {
            return null;
        }
        while (dataReader.Read())
        {

            Templates template = new Templates();

            //  Populate the template object with data from the data reader
            template.TemplateNo = dataReader["TemplateNo"].ToString();
            template.TemplateName = dataReader["TemplateName"].ToString();
            template.Description = dataReader["Description"].ToString();
            template.Email = dataReader["CreatorEmail"].ToString(); // Update column name to match database
            template.IsPublic = Convert.ToBoolean(dataReader["IsPublic"]); // Assuming IsPublic is a boolean in the database

            templatesList.Add(template);
        }

        if (con != null)
        {
            con.Close();
        }

        return templatesList;

    }

    SqlCommand buildReadStoredProcedureCommandTemplateByUser(SqlConnection con, string spName, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Email", email);
       
        return cmd;

    }


    //insert template
    public int InsertTemplate(Templates template)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertTemplateWithStoredProcedure("SP_insertTemplatesnew", con, template);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertTemplateWithStoredProcedure(String spName, SqlConnection con, Templates template)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Description", template.Description);
        cmd.Parameters.AddWithValue("@Email", template.Email);
        cmd.Parameters.AddWithValue("@IsPublic", template.IsPublic);
        cmd.Parameters.AddWithValue("@TemplateName", template.TemplateName);
        cmd.Parameters.AddWithValue("@TemplateNo", template.TemplateNo);

        return cmd;
    }



    //-------------------------------------------------------------------------------------
    //Langs
    //-------------------------------------------------------------------------------------
    public List<Langs> ReadLangs()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Langs> langsList = new List<Langs>();

        cmd = buildReadStoredProcedureCommandLangs(con, "SP_readAllLangs");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Langs l = new Langs();

            l.Language = dataReader["LangName"].ToString();
           
            langsList.Add(l);
        }

        if (con != null)
        {
            con.Close();
        }

        return langsList;

    }


    SqlCommand buildReadStoredProcedureCommandLangs(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }

    //-------------------------------------------------------------------------------------
    //Domains
    //-------------------------------------------------------------------------------------
    public List<Domains> ReadDomain()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Domains> domainsList = new List<Domains>();

        cmd = buildReadStoredProcedureCommandDomains(con, "SP_readAllDomains");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Domains d = new Domains();

            d.DomainName = dataReader["DomainName"].ToString();

            domainsList.Add(d);
        }

        if (con != null)
        {
            con.Close();
        }

        return domainsList;

    }


    SqlCommand buildReadStoredProcedureCommandDomains(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }



    //-------------------------------------------------------------------------------------
    //update block in template
    //-------------------------------------------------------------------------------------
    public int UpdateBlockInTemplate(BlockInTemplate block)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateUpdateBlockInTemplateWithStoredProcedure("SP_UpdateBlockInTemplate", con, block);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateUpdateBlockInTemplateWithStoredProcedure(String spName, SqlConnection con, BlockInTemplate block)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@TemplateNo", block.TemplateNo);
        cmd.Parameters.AddWithValue("@BlockNo", block.BlockNo);
        cmd.Parameters.AddWithValue("@Text", block.Text);


        return cmd;
    }






    //-------------------------------------------------------------------------------------
    //Block in templates
    //-------------------------------------------------------------------------------------

    public int InsertBlockInTemplate(BlockInTemplate block)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertBlockInTemplateWithStoredProcedure("SP_insertBlockInTemplate", con, block);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertBlockInTemplateWithStoredProcedure(String spName, SqlConnection con, BlockInTemplate block)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@TemplateNo", block.TemplateNo);
        cmd.Parameters.AddWithValue("@BlockNo", block.BlockNo);
        cmd.Parameters.AddWithValue("@Type", block.Type);
        cmd.Parameters.AddWithValue("@Title", block.Title);
        cmd.Parameters.AddWithValue("@Text", block.Text);
        cmd.Parameters.AddWithValue("@KeyWord", block.KeyWord);
        cmd.Parameters.AddWithValue("@IsActive", block.IsActive);
        cmd.Parameters.AddWithValue("@IsMandatory", block.IsMandatory);

        return cmd;
    }


    //-------------------------------------------------------------------------------------
    //read all blocks
    //-------------------------------------------------------------------------------------

    public List<BlockInTemplate> ReadBlockInTemplate()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<BlockInTemplate> BlockList = new List<BlockInTemplate>();

        cmd = buildReadStoredProcedureCommandBlockInTemplate(con, "SP_readAllBlockInTemplate");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            BlockInTemplate b = new BlockInTemplate();

            b.TemplateNo = dataReader["TemplateNo"].ToString();
            b.BlockNo = dataReader["BlockNo"].ToString();
            b.Type = dataReader["Type"].ToString();
            b.Title = dataReader["Title"].ToString();
            b.Text = dataReader["Text"].ToString();
            b.KeyWord = dataReader["KeyWord"].ToString();
            b.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
            b.IsMandatory = Convert.ToBoolean(dataReader["IsMandatory"]);
            BlockList.Add(b);
        }

        if (con != null)
        {
            con.Close();
        }

        return BlockList;

    }


    SqlCommand buildReadStoredProcedureCommandBlockInTemplate(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;



    }

    //-------------------------------------------------------------------------------------
    //get blocks by templateNo
    //-------------------------------------------------------------------------------------
    public List<BlockInTemplate> ReadBlockByTemplateNo(Templates template)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");  // Create the connection
        }
        catch (Exception ex)
        {
            // Write to log
            throw ex;
        }

        List<BlockInTemplate> BlockList = new List<BlockInTemplate>();

        cmd = buildReadStoredProcedureCommandReadBlockByTemplateNo(con, "SP_GetBlocksByTemplateNo");

        // Add parameter for template number
        cmd.Parameters.AddWithValue("@TemplateNo", template.TemplateNo);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            BlockInTemplate b = new BlockInTemplate();

            b.TemplateNo = dataReader["TemplateNo"].ToString();
            b.BlockNo = dataReader["BlockNo"].ToString();
            b.Type = dataReader["Type"].ToString();
            b.Title = dataReader["Title"].ToString();
            b.Text = dataReader["Text"].ToString();
            b.KeyWord = dataReader["KeyWord"].ToString();
            b.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
            b.IsMandatory = Convert.ToBoolean(dataReader["IsMandatory"]);
            BlockList.Add(b);
        }

        if (con != null)
        {
            con.Close();
        }

        return BlockList;
    }

    SqlCommand buildReadStoredProcedureCommandReadBlockByTemplateNo(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }


    //-------------------------------------------------------------------------------------
    //UserFavorites
    //-------------------------------------------------------------------------------------
    public List<UserFavorites> ReadUserFavorites()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<UserFavorites> usersFvorite = new List<UserFavorites>();

        cmd = buildReadUserFavoritesStoredProcedureCommand(con, "SP_readAllUserFavorite");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            UserFavorites u = new UserFavorites();
          
            u.Email = dataReader["email"].ToString();
            u.TemplateNo = dataReader["TemplateNo"].ToString();          
            usersFvorite.Add(u);
        }

        if (con != null)
        {
            con.Close();
        }

        return usersFvorite;

    }


    SqlCommand buildReadUserFavoritesStoredProcedureCommand(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }

    //insert fav template

    public int InsertFavTemplate(UserFavorites userFav)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertFavTemplateWithStoredProcedure("SP_insertFavTemplates", con, userFav);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertFavTemplateWithStoredProcedure(String spName, SqlConnection con, UserFavorites Favtemplate)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

      
        cmd.Parameters.AddWithValue("@Email", Favtemplate.Email);
        cmd.Parameters.AddWithValue("@TemplateNo", Favtemplate.TemplateNo);

        return cmd;
    }

    //read Fav templates
    public List<UserFavorites> ReadFavTemplateByUser(string email)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<UserFavorites> templatesList = new List<UserFavorites>();

        cmd = buildReadStoredProcedureCommandFavTemplateByUser(con, "SP_getFavTemplatesByEmail", email);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dataReader == null)
        {
            return null;
        }
        while (dataReader.Read())
        {

            UserFavorites template = new UserFavorites();

            //  Populate the template object with data from the data reader
            template.TemplateNo = dataReader["TemplateNo"].ToString();          
            templatesList.Add(template);
        }

        if (con != null)
        {
            con.Close();
        }

        return templatesList;

    }

    SqlCommand buildReadStoredProcedureCommandFavTemplateByUser(SqlConnection con, string spName, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Email", email);

        return cmd;

    }

    public int DeleteFavTemplateByUser(UserFavorites DeleteFavByUser)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = DeleteFavByUserWithStoredProcedure("SP_DeleteFavoriteByUserFromTbl", con, DeleteFavByUser);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand DeleteFavByUserWithStoredProcedure(String spName, SqlConnection con, UserFavorites DeleteFavByUser)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

       
        cmd.Parameters.AddWithValue("@Email", DeleteFavByUser.Email);
        cmd.Parameters.AddWithValue("@TemplateNo", DeleteFavByUser.TemplateNo);
        


        return cmd;
    }


    //insert block to summary
    public int InsertBlockInSummary(BlockInSummary block)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertBlockInSummaryWithStoredProcedure("SP_insertBlockInSummary", con, block);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertBlockInSummaryWithStoredProcedure(String spName, SqlConnection con, BlockInSummary block)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text


        cmd.Parameters.AddWithValue("@SummaryNo", block.SummaryNo);
        cmd.Parameters.AddWithValue("@BlockNo", block.BlockNo);
        cmd.Parameters.AddWithValue("@TemplateNo", block.TemplateNo);
        cmd.Parameters.AddWithValue("@text", block.Text);
        cmd.Parameters.AddWithValue("@IsApproved", block.IsApproved);


        return cmd;
    }

    //read summary
    public List<Summary> ReadSummary()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // Create the database connection
        }
        catch (Exception ex)
        {
            //  Handle the exception or log it
            throw (ex);
        }

        List<Summary> SummaryList = new List<Summary>();

        cmd = buildReadStoredProcedureCommandSummary(con, "SP_readAllSummary");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            Summary s = new Summary();

            //  Populate the template object with data from the data reader
            s.SummaryNo = dataReader["SummaryNo"].ToString();
            s.SummaryName = dataReader["SummaryName"].ToString();
            s.Comments = dataReader["Comments"].ToString();
            s.StartDateTime = Convert.ToDateTime(dataReader["StartDateTime"]);
            s.EndDateTime = Convert.ToDateTime(dataReader["EndDateTime"]);
            s.CreatorEmail = dataReader["CreatorEmail"].ToString();
            s.Descripton = dataReader["Descripton"].ToString();

            SummaryList.Add(s);
        }

        if (con != null)
        {
            con.Close();
        }

        return SummaryList;
    }


    SqlCommand buildReadStoredProcedureCommandSummary(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }


    //-------------------------------------------------------------------------------------
    //read all blocks in summary
    //-------------------------------------------------------------------------------------

    public List<BlockInSummary> ReadBlockInSummary()
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<BlockInSummary> BlockList = new List<BlockInSummary>();

        cmd = buildReadStoredProcedureCommandBlockInSummary(con, "SP_readAllBlockInSummary");

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            BlockInSummary b = new BlockInSummary();

            b.SummaryNo = dataReader["SummaryNo"].ToString();
            b.BlockNo = dataReader["BlockNo"].ToString();
            b.TemplateNo = dataReader["TemplateNo"].ToString();           
            b.Text = dataReader["Text"].ToString();
            b.IsApproved = Convert.ToBoolean(dataReader["IsApproved"]);

            BlockList.Add(b);
        }

        if (con != null)
        {
            con.Close();
        }

        return BlockList;

    }


    SqlCommand buildReadStoredProcedureCommandBlockInSummary(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;



    }


    //read summary by user
    public List<Summary> ReadSummaryByUser(string email)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Summary> SummaryList = new List<Summary>();

        cmd = buildReadStoredProcedureCommandSummaryByUser(con, "SP_getSummaryByEmail", email);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        if (dataReader == null)
        {
            return null;
        }
        while (dataReader.Read())
        {

            Summary s = new Summary();

            s.SummaryNo = dataReader["SummaryNo"].ToString();
            s.SummaryName = dataReader["SummaryName"].ToString();
            s.Comments = dataReader["Comments"].ToString();
            s.StartDateTime = Convert.ToDateTime(dataReader["StartDateTime"]);
            s.EndDateTime = Convert.ToDateTime(dataReader["EndDateTime"]);
            s.CreatorEmail = dataReader["CreatorEmail"].ToString();
            s.Descripton = dataReader["Descripton"].ToString();

            SummaryList.Add(s);
        }

        if (con != null)
        {
            con.Close();
        }

        return SummaryList;

    }

    SqlCommand buildReadStoredProcedureCommandSummaryByUser(SqlConnection con, string spName, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@Email", email);

        return cmd;

    }


    //insert Summary
    public int InsertSummary(Summary s)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateInsertSummaryWithStoredProcedure("SP_insertSummary", con, s);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateInsertSummaryWithStoredProcedure(String spName, SqlConnection con, Summary s)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@SummaryNo", s.SummaryNo);
        cmd.Parameters.AddWithValue("@SummaryName", s.SummaryName);
        cmd.Parameters.AddWithValue("@Comments", s.Comments);
        cmd.Parameters.AddWithValue("@StartDateTime", s.StartDateTime);
        cmd.Parameters.AddWithValue("@EndDateTime", s.EndDateTime);
        cmd.Parameters.AddWithValue("@CreatorEmail", s.CreatorEmail);
        cmd.Parameters.AddWithValue("@Descripton", s.Descripton);


        return cmd;
    }

    //-------------------------------------------------------------------------------------
    //get blocks by SummaryNo
    //-------------------------------------------------------------------------------------
    public List<BlockInSummary> ReadBlockBySummaryNo(Summary s)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB");  // Create the connection
        }
        catch (Exception ex)
        {
            // Write to log
            throw ex;
        }

        List<BlockInSummary> BlockList = new List<BlockInSummary>();

        cmd = buildReadStoredProcedureCommandReadBlockBySummaryNo(con, "SP_GetBlocksBySummaryNo");

        // Add parameter for Summary number
        cmd.Parameters.AddWithValue("@SummaryNo", s.SummaryNo);

        SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        while (dataReader.Read())
        {
            BlockInSummary b = new BlockInSummary();

            b.SummaryNo = dataReader["SummaryNo"].ToString();
            b.BlockNo = dataReader["BlockNo"].ToString();
            b.TemplateNo = dataReader["TemplateNo"].ToString();
            b.Text = dataReader["Text"].ToString();
            b.IsApproved = Convert.ToBoolean(dataReader["IsApproved"]);
           
            BlockList.Add(b);

        }

        if (con != null)
        {
            con.Close();
        }

        return BlockList;
    }

    SqlCommand buildReadStoredProcedureCommandReadBlockBySummaryNo(SqlConnection con, string spName)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;

    }


    //-------------------------------------------------------------------------------------
    //update block in Summary
    //-------------------------------------------------------------------------------------
    public int UpdateBlockInSummary(BlockInSummary block)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        cmd = CreateUpdateBlockInSummaryWithStoredProcedure("SP_UpdateBlockInSummary", con, block);     // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command- 0/1
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);

        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateUpdateBlockInSummaryWithStoredProcedure(String spName, SqlConnection con, BlockInSummary block)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@SummaryNo", block.SummaryNo);
        cmd.Parameters.AddWithValue("@BlockNo", block.BlockNo);
        cmd.Parameters.AddWithValue("@TemplateNo", block.TemplateNo);
        cmd.Parameters.AddWithValue("@Text", block.Text);


        return cmd;
    }





}
