INCLUDE globals.ink
{FistTalkMakoto == false: -> introduce | -> Test}


=== introduce ===
สวัสดีนายพึ่งเคยมาที่ Takuma ครั้งแรกยังงั้นหรอ
นายอยากจะให้ฉันแนะนำที่นี้ให้ไหม  ?
    + [ได้เลย]
        -> chosen("ได้เลย")
    + [ไม่ดีกว่า]
        -> chosen("ไม่ดีกว่า")
        
=== chosen(pokemon) ===
~ FistTalkMakoto = true
โอเค 

-> END

=== Test ===
ไม่อะ
-> END