import { A_OptionalDependent } from './a-optional-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';

@Entity('a_required_composite')
export class A_RequiredComposite {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  requiredCompAttr: string;

  @OneToOne(() => A_OptionalDependent, { cascade: true, eager: true })
  @JoinColumn()
  a_OptionalDependent?: A_OptionalDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}