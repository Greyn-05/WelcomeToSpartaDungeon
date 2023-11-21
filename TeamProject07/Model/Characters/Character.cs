using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject07.Characters
{
    internal class Character
    {
        Random rand = new Random();
        public string Name { get; set; }
        public int Level { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Hp { get; set; }

        public int MaxHp { get; set; }
        public int Gold { get; set; }

        public bool IsDead = false;

        public int Id { get; set; }
        /// <summary>
        /// 크리티컬 확률
        /// </summary>
        public int CritRate { get; set; }
        public int MissRate { get; set; }

        public int TakeDamage(Character enemy, int skill_damage)
        {
            // 데미지 오차 랜덤
            double damageRange = Math.Ceiling((double)((enemy.Attack+ skill_damage) / (double)10));
            int Damage = rand.Next((enemy.Attack + skill_damage) - (int)damageRange, (enemy.Attack + skill_damage) + (int)damageRange);

            //1.치명타인지 아닌지 확인
            if (rand.Next(1, 101) <= enemy.CritRate)
            {
                Damage = Damage * 2;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("치명타 발동");
                Console.ResetColor();
                
            }
            else
            {
                //2. 회피하였는지 아닌지 확인
                if (rand.Next(1, 101) <= MissRate)
                {
                    Damage = (int)Math.Ceiling((double)(Damage / 2));
                }
            }

            //받는 데미지에서 방어력 만큼 뺀 수치만큼 HP를 깎는다.
            if(Damage <= Defence)
            {
                Hp -= 1;
                Damage = 1;
            }
            else
            {
                Damage = Damage - Defence;
                Hp -= Damage;
            }
            if (Hp <= 0)
            {
                IsDead = true;
                Hp = 0;
            }

            return Damage;
        }

        public int Heal(int healVal)
        {
            if((Hp+healVal) >= MaxHp)
            {
                int temp = MaxHp - Hp;
                Hp = MaxHp;
                return temp;
            }
            else
            {
                Hp += healVal;
                return healVal;
            }
        }
        //Dead는 임시 보류
        public void Dead()
        {

            // TODO
        }
    }
}
