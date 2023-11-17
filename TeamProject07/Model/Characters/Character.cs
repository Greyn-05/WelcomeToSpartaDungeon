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
        public int Gold { get; set; }

        public bool IsDead = false;

        public int Id { get; set; }
        /// <summary>
        /// 크리티컬 확률
        /// </summary>
        public int CritRate { get; set; }
        public int MissRate { get; set; }

        public void TakeDamage(Character enemy) // 매개변수 임의로 작성
        {
            // 데미지 오차 랜덤
            int damageRange = (int)Math.Ceiling((double)(enemy.Attack / 10));
            int Damage = rand.Next(enemy.Attack - damageRange, enemy.Attack + damageRange);

            //1.치명타인지 아닌지 확인
            if (rand.Next(1, 101) > CritRate)
            {
                Damage = Damage * 2;
            }
            else
            {
                //2. 회피하였는지 아닌지 확인
                if (rand.Next(1, 101) > MissRate)
                {
                    Damage = (int)Math.Ceiling((double)(Damage / 2));
                }
            }

            //받는 데미지에서 방어력 만큼 뺀 수치만큼 HP를 깎는다.
            Hp -= (Damage - Defence);
            if (Hp <= 0)
            {
                IsDead = true;
            }
        }

        //Dead는 임시 보류
        public void Dead()
        {
            // TODO
        }
    }
}
