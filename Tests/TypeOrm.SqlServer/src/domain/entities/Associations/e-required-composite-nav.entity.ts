import { E_RequiredDependent } from './e-required-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToOne, JoinColumn } from 'typeorm';

@Entity('e_required_composite_nav')
export class E_RequiredCompositeNav {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  requiredCompNavAttr: string;

  @OneToOne(() => E_RequiredDependent, e_RequiredDependent => e_RequiredDependent.e_RequiredCompositeNav, { cascade: true, eager: true, nullable: false })
  @JoinColumn()
  e_RequiredDependent: E_RequiredDependent;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}