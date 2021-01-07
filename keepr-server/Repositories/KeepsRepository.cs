using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr_server.Models;

namespace keepr_server.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    private readonly string populateCreator = "SELECT keep.*, profile.* FROM keeps keep INNER JOIN profiles profile ON keep.creatorId = profile.id ";

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    public int Create(Keep newKeep)
    {
      string sql = @"
            INSERT INTO keeps 
            (name, description, creatorId, img, views, shares, keeps)
            VALUES
            (@Name, @Description, @CreatorId, @Img, @Views, @Shares, @Keeps);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }

    internal IEnumerable<Keep> getKeepsByProfile(string profId)
    {
      string sql = @"
        SELECT
        keep.*,
        p.*
        FROM keeps keep
        JOIN profiles p ON keep.creatorId = p.id
        WHERE keep.creatorId = @profId; ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { profId }, splitOn: "id");
    }

    internal bool Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1;";
      int affectedRows = _db.Execute(sql, new { id });
      // returns a bool
      return affectedRows > 0;
    }

    internal Keep GetOne(int id)
    {
      string sql = @"SELECT * from keeps WHERE id = @id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }

    internal void Edit(Keep editData)
    {
      string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
        WHERE id = @Id;";
      _db.Execute(sql, editData);
    }

    public IEnumerable<Keep> Get()
    {
      string sql = populateCreator;
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }
  }
}