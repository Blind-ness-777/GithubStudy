using System;

public class Solution {
    public bool solution(string s)
    {
        bool answer = true;
        
        int balance = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                balance++;
            }
            else if (s[i] == ')')
            {
                balance--;
            }

            if (balance < 0)
            {
                answer = false;
            }
        }
        if (balance != 0)
        {
            answer = true;
        }
        
        return answer;
    }
}