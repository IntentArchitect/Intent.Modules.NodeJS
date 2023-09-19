import { C_RequiredComposite } from './c-required-composite.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('c_multiple_dependent')
export class C_MultipleDependent {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  multipleDepAttr: string;

  @OneToMany(() => C_RequiredComposite, (c_RequiredComposites) => c_RequiredComposites.c_MultipleDependent, { cascade: true, eager: true })
  c_RequiredComposites: C_RequiredComposite[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}