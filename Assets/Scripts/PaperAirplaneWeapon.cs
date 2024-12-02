using UnityEngine;

public class PaperAirplaneWeapon : WeaponBaseScript
{
    public override void Attack()
    {
        GameObject current = Instantiate(bullet, Vector3.zero, Quaternion.identity);
        float[] stats = { damage * damageUpgrade, enemiesToPass, duration * durationUpgrade, speed * speedUpgrade, area * areaUpgrade, knockback };
        current.GetComponent<PaperAirplaneScript>().SetOwnStats(stats, infinitePass);
        current.GetComponent<PaperAirplaneScript>().mySpawner = this;
        current.GetComponent<PaperAirplaneScript>().SetSelfUp();
    }

    public override string LevelDescription(int level)
    {
        switch (level)
        {
            case 1:
                return "Orbits around the character.";
            case 2:
                return "Fires 1 more projectile.";
            case 3:
                return "Base Area up by 25%. Base Speed up by 30%";
            case 4:
                return "Effect lasts 0.5 seconds longer. Base Damage up by 10.";
            case 5:
                return "Fires 1 more projectile.";
            case 6:
                return "Base Area up by 25%. Base Speed up by 30%.";
            case 7:
                return "Effect lasts 0.5 seconds longer. Base Damage up by 10.";
            case 8:
                return "Fires 1 more projectile.";
            default:
                return "Error";
        }
    }

    public override void LevelSelfUp(int level) {
        switch (level)
        {
            case 2:
                numAttacks++;
                break;
            case 3:
                area += 0.25f;
                speed += 0.3f;
                break;
            case 4:
                duration += 0.5f;
                damage += 5;
                knockback -= 1f;
                break;
            case 5:
                numAttacks++;
                break;
            case 6:
                area += 0.25f;
                speed += 0.3f;
                break;
            case 7:
                duration += 0.5f;
                damage += 5;
                knockback -= 1f;
                break;
            case 8:
                numAttacks++;
                break;
            default:
                gameObject.SetActive(true);
                break;
        }
    }
}
