import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('j_required_dependent')
export class J_RequiredDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  reqDepAttr: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}