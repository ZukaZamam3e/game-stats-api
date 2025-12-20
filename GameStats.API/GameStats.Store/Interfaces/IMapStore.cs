using GameStats.Model;

namespace GameStats.Store.Interfaces;

public interface IMapStore
{
    Task<DataModel<MapModel>> GetMaps(PagedQuery<MapModel> pagedQuery);
    Task<MapModel?> GetMap(int mapId);
    Task<MapModel?> CreateMap(MapModel map);
    Task<MapModel?> UpdateMap(MapModel map);
    Task<bool> DeleteMap(int mapId);
}