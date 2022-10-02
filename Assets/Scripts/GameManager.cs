using UnityEngine;
public class GameManager : MonoBehaviour
{
    private const string PlayerCastleTag = "playerCastle";
    private const string AICastleTag = "aiCastle";

    [SerializeField]
    private static CastleUnit _playerCastle;
    [SerializeField]
    private static CastleUnit _aiCastle;

    private void Awake()
    {
        _playerCastle = GameObject.FindGameObjectWithTag(PlayerCastleTag)?.GetComponent<CastleUnit>();
        _aiCastle = GameObject.FindGameObjectWithTag(AICastleTag)?.GetComponent<CastleUnit>();

        if (_playerCastle is null || _aiCastle is null)
            throw new System.NullReferenceException("성채를 찾을 수 없습니다!");
    }
    /// <summary>
    /// 자신의 팀에 해당하는 성채 정보를 가져 옵니다.
    /// </summary>
    /// <param name="team">자신의 팀 정보를 제공해야 합니다.</param>
    /// <returns>인자와 같은 팀의 성채 유닛을 반환합니다.</returns>
    /// <exception cref="System.NotSupportedException">제공된 팀 정보에 대한 성체구현이 되지 않았거나, 팀 정보가 배당이 되지 않아 None으로 설정된 경우, 발생합니다.</exception>
    public static CastleUnit GetMyCastle(Team team)
    {
        switch (team)
        {
            case Team.Red:
                return _aiCastle;
            case Team.Blue:
                return _playerCastle;
            default:
                throw new System.NotSupportedException($"팀 정보 {team}에 대한 성채는 없습니다.");
        }
    }
    /// <summary>
    /// 적의 팀에 해당하는 성채 정보를 가져 옵니다.
    /// </summary>
    /// <param name="team">자신의 팀 정보를 제공해야 합니다.</param>
    /// <returns>인자와 반대되는 팀의 성채 유닛을 반환합니다.</returns>
    /// <exception cref="System.NotSupportedException">제공된 팀 정보에 대한 성체구현이 되지 않았거나, 팀 정보가 배당이 되지 않아 None으로 설정된 경우, 발생합니다.</exception>
    public static CastleUnit GetEnemyCastle(Team team)
    {
        switch (team)
        {
            case Team.Red:
                return _playerCastle;
            case Team.Blue:
                return _aiCastle;
            default:
                throw new System.NotSupportedException($"팀 정보 {team}에 대한 성채는 없습니다.");
        }
    }
}