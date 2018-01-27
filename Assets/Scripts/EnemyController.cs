using UnityEngine;

public class EnemyController : SceneryElement {

    const string MINION_TAG = "Pawn";
    const string BIG_BOSS_TAG = "BigKing";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collider_tag = collision.collider.tag;
        if (collider_tag.Equals(MINION_TAG))
        {
            collision.collider.GetComponent<PawnController>().ActDeath();
            Destroy(gameObject);
        }
        else if (collider_tag.Equals(BIG_BOSS_TAG))
        {
            collision.collider.GetComponent<BigBatController>().ActDeath();
            Destroy(gameObject);
        }
    }
}
